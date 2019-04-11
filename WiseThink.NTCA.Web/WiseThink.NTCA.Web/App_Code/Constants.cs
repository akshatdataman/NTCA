using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiseThink.NTCA.App_Code
{
    public class Constants
    {
        public const string INFO_MESSAGE = "infoMessage";
        public const string ERROR_MESSAGE = "errorMessage";
        public const string WARNING_MESSAGE = "warningMessage";

        // Form access levels
        public const int ACCESS_LEVEL_NO_ACCESS = 0;

        public const string CSSCLASS_VALIDATION_ERROR = "input-validation-error";
        public const string CSSCLASS_FORMFIELD = "textInput";
        public const string CSSCLASS_TEXT = "textInput";
        public const string CSSCLASS_DATEFIELD = "textInput datePicker";
        public const string CSSCLASS_TIMEFIELD = "textInput timeMask timeTemplates";

        public const string PATH_TO_FORMS = "Forms";
        public const string FORM_EXTENSION = "";

        public const string ROLE_ADMIN = "Admin";

        public readonly static string[] ImageExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
    }
}