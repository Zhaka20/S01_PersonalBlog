using S01_PersonalBlog.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S01_PersonalBlog.CustomValidators
{
    public enum FieldForValidation
    {
        Nickname,
        Email
    }


    public class AlreadyExists : ValidationAttribute
    {
        FieldForValidation _option;
        public AlreadyExists(FieldForValidation option)
        {
            _option = option;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                switch (_option)
                {
                    case FieldForValidation.Email:
                        using (var db = new ApplicationDbContext())
                        {
                            var exists = db.Users.Any(u => u.Email == value.ToString());
                            if (exists)
                            {
                                return new ValidationResult($"Email {value.ToString()} is already registered");
                            }
                        }
                        break;

                    case FieldForValidation.Nickname:
                        using (var db = new ApplicationDbContext())
                        {
                            var exists = db.Users.Any(u => u.NickName == value.ToString());
                            if (exists)
                            {
                                return new ValidationResult($"Nickname {value.ToString()} is already taken");
                            }
                        }
                        break;

                    default:
                        return ValidationResult.Success;
                }

            }
            return ValidationResult.Success;

        }


    }
    public class ValidTag : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var tagStr = value.ToString();
                var tagArr = tagStr.Split(',')
                                .Select(x => x.Trim())
                                .Where(x => !string.IsNullOrWhiteSpace(x))
                                .ToArray();
                if (tagArr.Length == 0)
                {
                    return new ValidationResult("At least one tag is required");
                }

                var tooLongTags = tagArr.Any(str => str.Length > 60);

                if (tooLongTags)
                {
                    return new ValidationResult("One or more tags length exceeds the limit of max 60 characters.");
                }
            }
            return ValidationResult.Success;
        }
    }
}