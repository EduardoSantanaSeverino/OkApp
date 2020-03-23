using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OkApp.ModelsBase;

namespace OkApp.Models
{
    public class Language : ModelWithoutTenant<int>
    {
        //
        // Summary:
        //     Gets or sets the name of the culture, like "en" or "en-US".
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        //
        // Summary:
        //     Gets or sets the display name.
        [Required]
        [StringLength(64)]
        public string DisplayName { get; set; }
        //
        // Summary:
        //     Gets or sets the icon.
        [StringLength(128)]
        public string Icon { get; set; }
        //
        // Summary:
        //     Is this language active. Inactive languages are not get by Abp.Localization.IApplicationLanguageManager.
        public bool IsDisabled { get; set; }

    }
}
