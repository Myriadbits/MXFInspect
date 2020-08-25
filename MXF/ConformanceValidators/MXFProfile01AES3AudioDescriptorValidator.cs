using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF.ConformanceValidators
{
    public class MXFProfile01AES3AudioDescriptorValidator : AbstractValidator<MXFAES3AudioEssenceDescriptor>
    {
        private enum ChannelStatusMode
        {
            STANDARD,
            MINIMUM
        }

        private readonly IDictionary<ChannelStatusMode, byte[]> validChannelStatusModes = new Dictionary<ChannelStatusMode, byte[]>()
        {
            { ChannelStatusMode.STANDARD, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x02 } }, // standard mode
            { ChannelStatusMode.MINIMUM , new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x01 } }  // minimum mode
        };

        private readonly IDictionary<ChannelStatusMode, byte[]> validChannelStatusDataForPCM = new Dictionary<ChannelStatusMode, byte[]>()
        {
            // standard mode, Professional Use, Linear PCM, No Emphasis, 48kHz Sampling, the CRCC value: 60
            { ChannelStatusMode.STANDARD,
                new byte[] {0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x18, 0x85, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60}
            },
            
            //  minimum mode, Professional Use, Linear PCM, No Emphasis, 48kHz Sampling
            { ChannelStatusMode.MINIMUM,
                new byte[] {0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x18, 0x85, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}
            }
        };

        private readonly byte[] validChannelStatusDataForDolby = new byte[]
        {
                // Professional Use, Non-PCM, No Emphasis, 48kHz Sampling, No indicated Channel Mode, 
                // No User Information indicated, Max. Audio Sample Word Length: 24 bit, Encoded Audio Sample Word Length: 
                // not indicated, CRCC value: 39
                 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x18, 0x83, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x39
        };


        public MXFProfile01AES3AudioDescriptorValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            // Essence Container Label [212W] (Audio essence mapping)
            // 060e2b34.04010101.0d010301.02060300(= MXF - GC Frame - wrapped AES3 audio data)
            RuleFor(desc => desc.EssenceContainer)
                .NotNull()
                .Equal(ConformanceValidationKeys.GC_FrameWrapped_AES3_AudioData_Key);

            // Sound Essence Coding [230W] / Sound Essence Compression
            RuleFor(desc => desc.SoundEssenceCoding)
                .NotNull()
                .Must(coding => IsValidSoundEssenceCoding(coding));

            // Sample Rate
            RuleFor(desc => desc.SampleRate)
                .NotNull()
                .Equal(new MXFRational { Num = 48000, Den = 1 });

            // TODO: Container Duration[9W]

            // Audio sampling rate[13W]
            RuleFor(desc => desc.AudioSamplingRate)
                .NotNull()
                .Equal(new MXFRational { Num = 48000, Den = 1 });

            // Locked/Unlocked [231W]
            RuleFor(desc => desc.Locked)
                .NotNull()
                .Equal(true);

            // TODO: Dial Norm, unclear specs
            // RuleFor(desc => desc.DialNorm)

            // TODO: Audio Ref Level
            // RuleFor(desc => desc.AudioRefLevel).NotNull()

            // Channel Count[164W]
            RuleFor(desc => desc.ChannelCount)
                .NotNull()
                .Equal((uint)1);

            // Quantization bits[3W]
            RuleFor(desc => desc.QuantizationBits)
                .NotNull()
                .Equal((uint)24);

            // Block Align[234W]
            RuleFor(desc => desc.BlockAlign)
                .NotNull()
                .Equal((ushort)3);

            // Average Bytes per Second(AvgBps) [235W]
            RuleFor(desc => desc.AveragesBytesPerSecond)
                .NotNull()
                .Equal((uint)144 * 1000);

            // TODO: Channel Status Mode(Byte Pattern) [236W], unclear specs
            RuleFor(desc => desc.ChannelStatusMode)
                .NotNull()
                .Must(mode => IsChannelSatusModeMinimum(mode) || IsChannelSatusModeStandard(mode));

            // Fixed Channel Status Data(for PCM Audio)[146W]
            // TODO: check if rule really checks what the specs demand
            RuleFor(desc => desc.FixedChannelStatusData)
                .NotNull()
                .Must(statusData => IsChannelStatusDataForPCM_Standard(statusData))
                .When(desc => !IsSoundEssenceDolbyCoded(desc.SoundEssenceCoding) &&
                              IsChannelSatusModeStandard(desc.ChannelStatusMode));

            // Fixed Channel Status Data (for PCM Audio) [146W]
            // TODO: check if rule really checks what the specs demand
            RuleFor(desc => desc.FixedChannelStatusData)
                .NotNull()
                .Must(statusData => IsChannelStatusDataForPCM_Minimum(statusData))
                .When(desc => !IsSoundEssenceDolbyCoded(desc.SoundEssenceCoding) &&
                              IsChannelSatusModeMinimum(desc.ChannelStatusMode));

            // Fixed Channel Status Data(for Dolby-E) [146W]
            RuleFor(desc => desc.FixedChannelStatusData)
                .NotNull()
                .Must(statusData => IsChannelStatusDataForDolbyValid(statusData))
                .When(desc => IsSoundEssenceDolbyCoded(desc.SoundEssenceCoding));
        }

        public bool IsSoundEssencePCMCoded(MXFKey coding)
        {
            return coding == ConformanceValidationKeys.PCM_SoundEssenceCoding_Key;
        }

        public bool IsSoundEssenceUndefindCoding(MXFKey coding)
        {
            return coding == ConformanceValidationKeys.Undefined_SoundEssenceCoding_Key;
        }

        public bool IsSoundEssenceDolbyCoded(MXFKey coding)
        {
            return coding == ConformanceValidationKeys.DolbyE_SoundEssenceCoding_Key;
        }

        public bool IsValidSoundEssenceCoding(MXFKey coding)
        {
            return IsSoundEssenceUndefindCoding(coding) ||
                    IsSoundEssencePCMCoded(coding) ||
                    IsSoundEssenceDolbyCoded(coding);
        }

        public bool IsChannelStatusDataForPCM_Standard(byte[] channelStatusData)
        {
            return channelStatusData.SequenceEqual(validChannelStatusDataForPCM[ChannelStatusMode.STANDARD]);
        }

        public bool IsChannelStatusDataForPCM_Minimum(byte[] channelStatusData)
        {
            return channelStatusData.SequenceEqual(validChannelStatusDataForPCM[ChannelStatusMode.MINIMUM]);
        }

        public bool IsChannelSatusModeStandard(byte[] channelStatusMode)
        {
            return channelStatusMode.SequenceEqual(validChannelStatusModes[ChannelStatusMode.STANDARD]);
        }

        public bool IsChannelSatusModeMinimum(byte[] channelStatusMode)
        {
            return channelStatusMode.SequenceEqual(validChannelStatusModes[ChannelStatusMode.MINIMUM]);
        }

        public bool IsChannelStatusDataForDolbyValid(byte[] channelStatusData)
        {
            return channelStatusData.SequenceEqual(validChannelStatusDataForDolby);
        }

    }
}
