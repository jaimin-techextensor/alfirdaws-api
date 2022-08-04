using System;
using System.Collections.Generic;

namespace alfirdawsmanager.Data.Models
{
    public partial class Language
    {
        public int LanguageId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public byte[]? Icon { get; set; }
        public string? TranslationFile { get; set; }
        public bool? Active { get; set; }
    }
}
