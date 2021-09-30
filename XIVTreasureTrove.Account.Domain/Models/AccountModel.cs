using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIVTreasureTrove.Account.Domain.Abstracts;

namespace XIVTreasureTrove.Account.Domain.Models
{
    /// <summary>
    /// The Account model 
    /// </summary>
    public class AccountModel : AEntity, IValidatableObject
    {
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Email address required")]
        [EmailAddress(ErrorMessage = "Must be a real email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username required")]
        [MaxLength(20, ErrorMessage = "Max length of 20 characters")]
        public string Username { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public AccountModel() { }

        /// <summary>
        /// Constructor that accepts username and email values
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        public AccountModel(string username, string email)
        {
            Username = username;
            Email = email;
            IsActive = true;
        }

        [RegularExpression(@"^(http(s?):\/\/)[^\s]*$", ErrorMessage = "Image URI must be a real image URI.")]
        public string ImageUri { get; set; } = "https://bulma.io/images/placeholders/256x256.png";

        /// <summary>
        /// The Account 'Validate' method
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
