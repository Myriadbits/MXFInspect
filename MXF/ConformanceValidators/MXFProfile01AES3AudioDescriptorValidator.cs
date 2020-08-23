using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Myriadbits.MXF.ConformanceValidators
{
    public class MXFProfile01AES3AudioDescriptorValidator : AbstractValidator<MXFAES3AudioEssenceDescriptor>
    {
        private readonly IEnumerable<MXFKey> validSoundEssenceCodings = new List<MXFKey>()
        {
                ConformanceValidationKeys.PCM_SoundEssenceCoding_Key,
                ConformanceValidationKeys.Uncompressed_SoundEssenceCoding_Key,
                ConformanceValidationKeys.DolbyE_SoundEssenceCoding_Key
        };

        private readonly IDictionary<string, byte[]> validChannelStatusModes = new Dictionary<string, byte[]>()
        {
            { "standard", new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x02 } }, // standard mode
            { "minimum" , new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x01 } }  // minimum mode
        };

        private readonly IDictionary<string, byte[]> validChannelStatusDataForPCM = new Dictionary<string, byte[]>()
        {
            // standard mode, Professional Use, Linear PCM, No Emphasis, 48kHz Sampling, the CRCC value: 60
            { "standard",
                new byte[] {0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x18, 0x85, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60}
            },
            
            //  minimum mode, Professional Use, Linear PCM, No Emphasis, 48kHz Sampling)
            { "minimum",
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
            // Essence Container Label [212W] (Audio essence mapping)
            // 060e2b34.04010101.0d010301.02060300(= MXF - GC Frame - wrapped AES3 audio data)
            RuleFor(desc => desc.EssenceContainer).Equal(ConformanceValidationKeys.GC_FrameWrapped_AES3_AudioData_Key);

            // Sound Essence Coding [230W] / Sound Essence Compression
            RuleFor(desc => desc.SoundEssenceCoding).Must(c => validSoundEssenceCodings.Contains(c));

            // Sample Rate
            RuleFor(desc => desc.SampleRate).NotNull().Equal(new MXFRational { Num = 48000, Den = 1 });

            // TODO: Container Duration[9W]

            // Audio sampling rate[13W]
            RuleFor(desc => desc.AudioSamplingRate).NotNull().Equal(new MXFRational { Num = 48000, Den = 1 });

            // Locked/Unlocked [231W]
            RuleFor(desc => desc.Locked).NotNull().Equal(true);

            // TODO: Dial Norm, unclear specs
            // RuleFor(desc => desc.DialNorm)

            // TODO: Audio Ref Level
            // RuleFor(desc => desc.AudioRefLevel).NotNull()

            // Channel Count[164W]
            RuleFor(desc => desc.ChannelCount).NotNull().Equal((uint)1);

            // Quantization bits[3W]
            RuleFor(desc => desc.QuantizationBits).NotNull().Equal((uint)24);

            // Block Align[234W]
            RuleFor(desc => desc.BlockAlign).NotNull().Equal((ushort)3);

            // Average Bytes per Second(AvgBps) [235W]
            RuleFor(desc => desc.AveragesBytesPerSecond).NotNull().Equal((uint)144 * 1000);

            // TODO: Channel Status Mode(Byte Pattern) [236W], unclear specs
            RuleFor(desc => desc.ChannelStatusMode).Must(csm => validChannelStatusModes.Values.Any(v => v.SequenceEqual(csm)));

            // Fixed Channel Status Data (for PCM Audio) [146W]
            RuleFor(desc => desc.FixedChannelStatusData)
                .NotNull()
                .Must(csd => csd.SequenceEqual(validChannelStatusDataForPCM["standard"]))
                .When(desc => desc.SoundEssenceCoding.Equals(ConformanceValidationKeys.PCM_SoundEssenceCoding_Key) &&
                              desc.ChannelStatusMode.SequenceEqual(validChannelStatusModes["standard"]));

            RuleFor(desc => desc.FixedChannelStatusData)
                .NotNull()
                .Must(csd => csd.SequenceEqual(validChannelStatusDataForPCM["minimum"]))
                .When(desc => desc.SoundEssenceCoding.Equals(ConformanceValidationKeys.PCM_SoundEssenceCoding_Key) &&
                              desc.ChannelStatusMode.SequenceEqual(validChannelStatusModes["minimum"]));

            // Fixed Channel Status Data(for Dolby-E) [146W]
            RuleFor(desc => desc.FixedChannelStatusData)
                .NotNull()
                .Must(csd => csd.SequenceEqual(validChannelStatusDataForDolby))
                .When(desc => desc.SoundEssenceCoding.Equals(ConformanceValidationKeys.DolbyE_SoundEssenceCoding_Key));
        }

    }
}
