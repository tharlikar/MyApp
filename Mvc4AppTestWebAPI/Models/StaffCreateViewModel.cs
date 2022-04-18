using com.minsoehanwin.sample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mvc4AppTestWebAPI.ExtensionLib;
namespace Mvc4AppTestWebAPI.Models
{
    public class StaffViewModelValidateFirstNameAttribute : ValidationAttribute
    {
        private EmployeeValidator _employeeValidator;
        private EmployeeValidator EmployeeValidator{
            get { 
                if (_employeeValidator == null)
                {
                    _employeeValidator = HttpContext.Current.GetMyInstance<EmployeeValidator>();
                }
                return _employeeValidator;
            }
            set{
                _employeeValidator = value;
            }
        }
        public StaffViewModelValidateFirstNameAttribute()
            : base()
        {
        }
        public StaffViewModelValidateFirstNameAttribute(string errorMsg)
            : base(errorMsg)
        {
        }
        public override bool IsValid(object value)
        {
            try
            {
                EmployeeValidator.ValidateEmployeeFirstName(value.ToString());
                return true;
            }
            catch (Exception ex)
            {
                if(string.IsNullOrEmpty(this.ErrorMessage))
                    this.ErrorMessage = ex.Message;
                return false;
            }
        }
    }

    public class StaffCreateViewModel
    {
        public int? Id { get; set; }
        
        [Display(Name = "First Name")]
        [Required]
        [StaffViewModelValidateFirstName(ErrorMessage="Your custom validation say FirstName is less than 3 chars.")]
        //[StaffViewModelValidateFirstName()]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        [Required]
        [StaffViewModelValidateFirstName()]
        public string LastName { get; set; }
        
        [Display(Name = "Store")]
        public int? MyStoreId { get; set; }

        [Display(Name = "Passport No.")]
        [Required]
        public virtual string PassportNo { get; set; }

        [Display(Name = "Issue Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime IssueDate { get; set; }

        [Display(Name = "Expired Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime ExpiredDate { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StaffViewModelValidateFirstName()]
        public virtual string WifeFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StaffViewModelValidateFirstName()]
        public virtual string WifeLastName { get; set; }
    }
}