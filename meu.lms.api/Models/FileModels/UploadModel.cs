using meu.lms.api.Enums;

namespace meu.lms.api.Models.FileModels
{
    public class UploadModel
    {
        public string NewName { get; set; }
        public string ErrorMessage { get; set; }
        public string WarningMessage { get; set; } // Yalnızca güncelleme işleminde kullanılır. 
        public UploadState UploadState { get; set; }
    }
}