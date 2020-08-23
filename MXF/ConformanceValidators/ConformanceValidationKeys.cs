using System;
using System.Collections.Generic;
using System.Text;

namespace Myriadbits.MXF.ConformanceValidators
{
    public static class ConformanceValidationKeys
    {
        private static readonly int[] firstKey = new int[] { 0x06, 0x0e, 0x2b, 0x34, 0x01, 0x01, 0x01, 0x05 };

        //
        private static readonly int[] codingKey1 = new int[] { 0x06, 0x0E, 0x2B, 0x34, 0x04, 0x01, 0x01, 0x03 };
        private static readonly int[] codingKey2 = new int[] { 0x04, 0x01, 0x02, 0x02, 0x01, 0x04, 0x03, 0x00 };
        public static readonly MXFKey MPEG2_422P_HL_Long_GOP_Coding_Key = new MXFKey(codingKey1, codingKey2);

        //
        private static readonly int[] transferCharKey1 = new int[] { 0x06, 0x0E, 0x2B, 0x34, 0x04, 0x01, 0x01, 0x01 };
        private static readonly int[] transferCharKey2 = new int[] { 0x04, 0x01, 0x01, 0x01, 0x01, 0x02, 0x00, 0x00 };
        public static readonly MXFKey ITU_R_BT_709_Transfer_Characteristic_Key = new MXFKey(transferCharKey1, transferCharKey2);

        // 
        private static readonly int[] frameWrappedMappingKey1 = new int[] { 0x06, 0x0E, 0x2B, 0x34, 0x04, 0x01, 0x01, 0x02 };
        private static readonly int[] frameWrappedMappingKey2 = new int[] { 0x0D, 0x01, 0x03, 0x01, 0x02, 0x04, 0x60, 0x01 };
        public static readonly MXFKey GC_FrameWrapped_MPEG_VideoStream0_SID_Key = new MXFKey(frameWrappedMappingKey1, frameWrappedMappingKey2);

        // PCM: 060e2b34.0401010A.04020201.01000000(= PCM)
        private static readonly int[] pcmCodingKey1 = new int[] { 0x06, 0x0e, 0x2b, 0x34, 0x04, 0x01, 0x01, 0x0A };
        private static readonly int[] pcmCodingKey2 = new int[] { 0x04, 0x02, 0x02, 0x01, 0x01, 0x00, 0x00, 0x00 };
        public static readonly MXFKey PCM_SoundEssenceCoding_Key = new MXFKey(pcmCodingKey1, pcmCodingKey2);

        // Undefined: 060e2b34.04010101.04020201.7f000000 (= Uncompressed Sound Coding, Undefined Sound Coding) 
        private static readonly int[] uncompressedCodingKey1 = new int[] { 0x06, 0x0e, 0x2b, 0x34, 0x04, 0x01, 0x01, 0x01 };
        private static readonly int[] uncompressedCodingKey2 = new int[] { 0x04, 0x02, 0x02, 0x01, 0x7f, 0x00, 0x00, 0x00 };
        public static readonly MXFKey Uncompressed_SoundEssenceCoding_Key = new MXFKey(uncompressedCodingKey1, uncompressedCodingKey2);

        // Dolby-E: 060e2b34.04010101.04020202.03021c00(= Dolby-E Compressed Audio)
        private static readonly int[] dolbyECodingKey1 = new int[] { 0x06, 0x0e, 0x2b, 0x34, 0x04, 0x01, 0x01, 0x01 };
        private static readonly int[] dolbyECodingKey2 = new int[] { 0x04, 0x02, 0x02, 0x02, 0x03, 0x02, 0x1c, 0x00 };
        public static readonly MXFKey DolbyE_SoundEssenceCoding_Key = new MXFKey(dolbyECodingKey1, dolbyECodingKey2);

        // 060e2b34.04010101.0d010301.02060300(= MXF - GC Frame - wrapped AES3 audio data)
        private static readonly int[] frameWrappedAudioDataKey1 = new int[] { 0x06, 0x0e, 0x2b, 0x34, 0x04, 0x01, 0x01, 0x01 };
        private static readonly int[] frameWrappedAudioDataKey2 = new int[] { 0x0d, 0x01, 0x03, 0x01, 0x02, 0x06, 0x03, 0x00 };
        public static readonly MXFKey GC_FrameWrapped_AES3_AudioData_Key = new MXFKey(frameWrappedAudioDataKey1, frameWrappedAudioDataKey2);

        // keys for local tags in Picture Descriptor
        public static readonly MXFKey BitRate_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x0b, 0x00, 0x00 });
        public static readonly MXFKey IdenticalGOPIndicator_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x07, 0x00, 0x00 });
        public static readonly MXFKey MaxGOPSize_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x08, 0x00, 0x00 });
        public static readonly MXFKey MaxBPictureCount_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x09, 0x00, 0x00 });
        public static readonly MXFKey ConstantBPictureFlag_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x03, 0x00, 0x00 });
        public static readonly MXFKey ContentScanningKind_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x04, 0x00, 0x00 });
        public static readonly MXFKey ProfileAndLevel_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x0a, 0x00, 0x00 });
        public static readonly MXFKey SingleSequenceFlag_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x02, 0x00, 0x00 });
        public static readonly MXFKey ClosedGOP_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x06, 0x00, 0x00 });
        public static readonly MXFKey LowDelay_Key = new MXFKey(firstKey, new int[] { 0x04, 0x01, 0x06, 0x02, 0x01, 0x05, 0x00, 0x00 });


        // 00 00 00 01 00 00 00 18 85 00 04 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 60 (= standard mode, Professional Use, Linear PCM, No Emphasis, 48kHz Sampling, the CRCC value: 60)
        private static readonly int[] frameWrappedAudioData =
        {0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x18, 0x85, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00,
         0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60};

        //00 00 00 01 00 00 00 18 85 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 (= minimum mode, Professional Use, Linear PCM, No Emphasis, 48kHz Sampling)








    }
}
