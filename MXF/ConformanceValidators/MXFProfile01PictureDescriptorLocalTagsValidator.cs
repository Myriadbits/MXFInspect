using System.Collections.Generic;
using System.Linq;
using FluentValidation;



namespace Myriadbits.MXF.ConformanceValidators
{
    public class MXFProfile01PictureDescriptorLocalTagsValidator : AbstractValidator<IEnumerable<MXFLocalTag>>
    {
        public MXFProfile01PictureDescriptorLocalTagsValidator(MXFCDCIPictureEssenceDescriptor desc)
        {
            MXFLocalTag bitRate_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.BitRate_Key);
            MXFLocalTag identicalGOP_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.IdenticalGOPIndicator_Key);
            MXFLocalTag maxGOPSize_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.MaxGOPSize_Key);
            MXFLocalTag maxBPicCount_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.MaxBPictureCount_Key);
            MXFLocalTag constBPicFlag_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.ConstantBPictureFlag_Key);
            MXFLocalTag contentScanningKind_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.ContentScanningKind_Key);
            MXFLocalTag profileAndLevel_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.ProfileAndLevel_Key);
            MXFLocalTag singleSequenceFlag_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.SingleSequenceFlag_Key);
            MXFLocalTag closedGOP_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.ClosedGOP_Key);
            MXFLocalTag lowDelay_Tag = GetLocalTagByAliasUID(desc, ConformanceValidationKeys.LowDelay_Key);


            CascadeMode = CascadeMode.StopOnFirstFailure;

            // Bit Rate[158W], 50_000_000
            RuleFor(tags => GetTagValue(bitRate_Tag))
                .NotNull()
                .WithName("Bit Rate [158W]")
                .Equals((uint)50 * 1000 * 1000);

            // Identical GOP Indicator [224W], [False, True]
            RuleFor(tags => GetTagValue(identicalGOP_Tag))
                            .NotNull()
                            .WithName("Identical GOP Indicator [224W]")
                            .Must(v => v.Equals((byte)0) || v.Equals((byte)1));

            // Maximum GOP Size [46W], (ushort)12
            RuleFor(tags => GetTagValue(maxGOPSize_Tag))
                .NotNull()
                .WithName("Maximum GOP Size [46W]")
                .Equals((ushort)12);

            // Maximum B Picture Count [46W], (ushort)2
            RuleFor(tags => GetTagValue(maxBPicCount_Tag))
                            .NotNull()
                            .WithName("Maximum B Picture Count [46W]")
                            .Equals((ushort)2);

            // Constant B Picture Flag [225W], [False, True]
            RuleFor(tags => GetTagValue(constBPicFlag_Tag))
                            .NotNull()
                            .WithName("Constant B Picture Flag [225W]")
                            .Must(v => v.Equals((byte)0) || v.Equals((byte)1));

            // Coded Content Scanning Kind [226W], 0x800E, (byte)2
            RuleFor(tags => GetTagValue(contentScanningKind_Tag))
                .NotNull()
                .WithName("Coded Content Scanning Kind [226W]")
                .Equals((byte)2);

            // Profile And Level [35W], (byte)130 
            RuleFor(tags => GetTagValue(profileAndLevel_Tag))
                            .NotNull()
                            .WithName("Profile And Level [35W]")
                            .Equals((byte)130);

            // Single Sequence Flag [243W], [False, True]
            RuleFor(tags => GetTagValue(singleSequenceFlag_Tag))
                            .NotNull()
                            .WithName("Single Sequence Flag [243W]")
                            .Must(v => v.Equals((byte)0) || v.Equals((byte)1));

            // Closed GOP Indicator [45W], [False, True]
            RuleFor(tags => GetTagValue(closedGOP_Tag))
                .NotNull()
                .WithName("Closed GOP Indicator [45W]")
                .Must(v => v.Equals((byte)0) || v.Equals((byte)1));

            // Low Delay Indicator [227W], (byte)0
            RuleFor(tags => GetTagValue(lowDelay_Tag))
                .NotNull()
                .WithName("Low Delay Indicator [227W]")
                .Equals((byte)0);
        }

        private MXFLocalTag GetLocalTagByAliasUID(MXFCDCIPictureEssenceDescriptor desc, MXFKey key)
        {
            var localTagKey = GetLocalTagKeyByAliasUID(desc, key);
            if (key != null)
            {
                return desc.Children.OfType<MXFLocalTag>().FirstOrDefault(t => t.Tag == localTagKey);
            }
            else return null;
        }

        private ushort? GetLocalTagKeyByAliasUID(MXFCDCIPictureEssenceDescriptor desc, MXFKey key)
        {
            var file = desc.TopParent as MXFFile;
            if (file != null)
            {
                return file.FlatList
                    .OfType<MXFEntryPrimer>()?
                    .FirstOrDefault(e => e.AliasUID.Key == key)?
                    .LocalTag;
            }
            else
            {
                return null;
            }
        }

        private object GetTagValue(MXFLocalTag tag)
        {
            return tag?.Value;
        }
    }
}
