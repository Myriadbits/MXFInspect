using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;



namespace Myriadbits.MXF.ConformanceValidators
{
    public class MXFProfile01PictureDescriptorValidator : AbstractValidator<MXFCDCIPictureEssenceDescriptor>
    {
        private readonly IEnumerable<byte> validActiveFormatDescriptors = new List<byte>()
        {
            0b00000100,  // undefined, aspect ratio 16:9
            0b00100100,  // letterbox, aspect ratio 16:9
            0b01000100,  // full frame, aspect ratio 16:9
            0b01001100,  // pillarbox, aspect ratio 16:9
        };

        public MXFProfile01PictureDescriptorValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            // Essence Container Label(Video essence mapping)[212W]
            RuleFor(desc => desc.EssenceContainer)
                .NotNull()
                .Equal(ConformanceValidationKeys.GC_FrameWrapped_MPEG_VideoStream0_SID_Key)
                .WithName("Essence Container Label(Video essence mapping)[212W]");

            // TODO Picture Element Key[165W]
            // 060e2b34.01020101.0d010301.15010500(= MXF Generic Container Version 1 SMPTE 381M MPEG Frame - Wrapped Picture Essence)
            //RuleFor(desc => desc).Equal(ConformanceValidationKeys.GC_FrameWrapped_MPEG_VideoStream0_SID_Key);

            // Picture Essence Coding [105W]
            RuleFor(desc => desc.PictureEssenceCoding)
                .NotNull()
                .Equal(ConformanceValidationKeys.MPEG2_422P_HL_Long_GOP_Coding_Key)
                .WithName("Picture Essence Coding [105W]");

            // Aspect Ratio [69W]
            RuleFor(desc => desc.AspectRatio)
                .NotNull()
                .Equal(new MXFRational { Num = 16, Den = 9 })
                .WithName("Aspect Ratio [69W]");

            // Sample Rate [42W]
            RuleFor(desc => desc.SampleRate)
                .NotNull()
                .Equal(new MXFRational { Num = 25, Den = 1 })
                .WithName("Sample Rate [42W]");

            // TODO: Container Duration[40W]
            // Shall be present and identical with audio Container Duration. 
            // If the partition status is incomplete, the value may be absent.

            // Field Dominance [41W]
            RuleFor(desc => desc.FieldDominance)
                .NotNull()
                .Equal((byte?)1)
                .WithName("Field Dominance [41W]");

            // Signal Standard [162W] 
            RuleFor(desc => desc.SignalStandard)
                .NotNull()
                .Equal((byte?)4)
                .WithName("Signal Standard [162W]");

            // Frame Layout [214W]
            RuleFor(desc => desc.FrameLayout)
                .NotNull()
                .Equal((byte?)1)
                .WithName("Frame Layout [214W]");

            // Display Width [43W]
            RuleFor(desc => desc.DisplayWidth)
                .NotNull()
                .Equal((uint?)1920)
                .WithName("Display Width [43W]");

            // Display Height [43W]
            RuleFor(desc => desc.DisplayHeight)
                .NotNull()
                .Equal((uint?)540)
                .WithName("Display Height [43W]");

            // Sample Width [163W]
            RuleFor(desc => desc.SampledWidth)
                .NotNull()
                .Equal((uint?)1920)
                .WithName("Sample Width [163W]");

            // Sample Height [163W]
            RuleFor(desc => desc.SampledHeight)
                .NotNull()
                .Equal((uint?)540)
                .WithName("Sample Height [163W]");

            // Stored Width [70W]
            RuleFor(desc => desc.StoredWidth)
                .NotNull()
                .Equal((uint?)1920)
                .WithName("Stored Width [70W]");

            // Stored Height [70W]
            RuleFor(desc => desc.StoredHeight)
                .NotNull().Equal((uint?)544)
                .WithName("Stored Height [70W]");

            // Stored F2 Offset [161W]
            RuleFor(desc => desc.StoredF2Offset)
                .NotNull()
                .Equal(0)
                .WithName("Stored F2 Offset [161W]");

            // Sampled X Offset [161W]
            RuleFor(desc => desc.SampledXOffset)
                .NotNull()
                .Equal(0)
                .WithName("Sampled X Offset [161W]");

            // Sampled Y Offset [161W]
            RuleFor(desc => desc.SampledYOffset)
                .NotNull()
                .Equal(0)
                .WithName("Sampled Y Offset [161W]");

            // Display X Offset [161W]
            RuleFor(desc => desc.DisplayXOffset)
                .NotNull()
                .Equal(0)
                .WithName("Display X Offset [161W]");

            // Display Y Offset [161W]
            RuleFor(desc => desc.DisplayYOffset)
                .NotNull()
                .Equal(0)
                .WithName("Display Y Offset [161W]");

            // Display F2 Offset [161W]
            RuleFor(desc => desc.DisplayF2Offset)
                .NotNull()
                .Equal(0)
                .WithName("Display F2 Offset [161W]");

            // TODO: optimize values as list of possible values
            // Active Format Descriptor [1W]
            RuleFor(desc => desc.ActiveFormatDescriptor)
                .NotNull()
                .Must(afd => IsValidActiveFormatDescriptor((byte)afd))
                .WithName("Active Format Descriptor [1W]");

            // Video Line Map [159W]
            RuleFor(desc => desc.VideoLineMap)
                .NotNull()
                .Must(vlm => IsValidVideoLineMap(vlm))
                .WithName("Video Line Map [159W]");

            // Transfer Characteristic / Capture Gamma [215W]
            RuleFor(desc => desc.TransferCharacteristics)
                .NotNull()
                .Equal(ConformanceValidationKeys.ITU_R_BT_709_Transfer_Characteristic_Key)
                .WithName("Transfer Characteristic / Capture Gamma [215W]");

            // Signal Standard[162W]
            RuleFor(desc => desc.SignalStandard)
                 .NotNull()
                 .Equal((byte?)4)
                 .WithName("Signal Standard[162W]");

            // Image Start Offset [216W]
            RuleFor(desc => desc.ImageStartOffset)
                .NotNull()
                .Equal((uint?)0)
                .WithName("Image Start Offset [216W]");

            // Image End Offset [237W]
            RuleFor(desc => desc.ImageEndOffset)
                .NotNull()
                .Equal((uint?)0)
                .WithName("Image End Offset [237W]");

            // Color Siting [217W]
            RuleFor(desc => desc.ColorSiting)
                .NotNull()
                .Equal((byte?)0)
                .WithName("Color Siting [217W]");

            // Padding Bits [218W]
            RuleFor(desc => desc.PaddingBits)
                .NotNull()
                .Equal((short?)0)
                .WithName("Padding Bits [218W]");

            // Black Ref Level [219W]
            RuleFor(desc => desc.BlackRefLevel)
                .NotNull()
                .Equal((uint?)16)
                .WithName("Black Ref Level [219W]");

            // White Ref Level [220W]
            RuleFor(desc => desc.WhiteRefLevel)
                .NotNull()
                .Equal((uint?)235)
                .WithName("White Ref Level [220W]");

            // Color Range [221W]
            RuleFor(desc => desc.ColorRange)
                .NotNull()
                .Equal((uint?)225)
                .WithName("Color Range [221W]");

            // Horizontal Subsampling [34W]
            RuleFor(desc => desc.HorizontalSubsampling)
                .NotNull()
                .Equal((uint?)2)
                .WithName("Horizontal Subsampling [34W]");

            // Vertical Subsampling [34W]
            RuleFor(desc => desc.VerticalSubsampling)
                .NotNull()
                .Equal((uint?)1)
                .WithName("Vertical Subsampling [34W]");

            // Component Depth [32W]
            RuleFor(desc => desc.ComponentDepth)
                .NotNull()
                .Equal((uint?)8)
                .WithName("Component Depth [32W]");

            // validate local tags
            RuleFor(desc => desc.Children.OfType<MXFLocalTag>())
                .SetValidator(desc => new MXFProfile01PictureDescriptorLocalTagsValidator(desc));
        }

        public bool IsValidActiveFormatDescriptor(byte formatDescriptor)
        {
            return validActiveFormatDescriptors.Contains(formatDescriptor);
        }

        public bool IsValidVideoLineMap(int[] videoLineMap)
        {
            return videoLineMap.SequenceEqual(new int[] { 2, 4, 21, 584 });
        }
    }
}
