using FluentValidation;
using Myriadbits.MXF.ConformanceValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myriadbits.MXF.ConformanceValidators
{
    
    public class MXFProfile01Validator : AbstractValidator<MXFFile>
    {
        public MXFProfile01Validator(MXFFile f)
        {
            // TODO: check file structure (partitions, the beginning of the PDF specs)

            // TODO: check all video and audio essences and wrappings 

            // check all video specs
            RuleFor(file => file.GetMXFPictureDescriptorInHeader()).SetValidator(new MXFProfile01PictureDescriptorValidator());

            // check all audio specs
            var aes3descriptors = f.GetMXFAES3AudioEssenceDescriptor();
            RuleForEach(file => aes3descriptors).SetValidator(new MXFProfile01AES3AudioDescriptorValidator());
        }
    }
}
