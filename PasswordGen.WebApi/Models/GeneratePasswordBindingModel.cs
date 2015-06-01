namespace PasswordGen.WebApi.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class GeneratePasswordBindingModel
    {
        public GeneratePasswordBindingModel()
        {
            this.Length = 8;
            this.Lowercase = true;
            this.Uppercase = true;
            this.Digits = true;
        }

        [Required]
        [Range(1, 100)]
        [DefaultValue(8)]
        [Display(Name = "Password length")]
        public int Length { get; set; }

        [Required]
        [DefaultValue(true)]
        [Display(Name = "Include lowercase letters (a-z)?")]
        public bool Lowercase { get; set; }

        [Required]
        [DefaultValue(true)]
        [Display(Name = "Include uppercase letters (A-Z)?")]
        public bool Uppercase { get; set; }

        [Required]
        [DefaultValue(true)]
        [Display(Name = "Include digits (0-9)?")]
        public bool Digits { get; set; }

        [Required]
        [Display(Name = "Include special characters (e.g. !\"#$)?")]
        public bool Special { get; set; }
    }
}