using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using meu.lms.api.Enums;
using meu.lms.api.Models.FileModels;
using meu.lms.dataaccess.Interfaces;
using meu.lms.entities.Concrete;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        public IAssignmentDal _assignmentDal { get; set; }
        public BaseController(IAssignmentDal assignmentDal)
        {
            _assignmentDal = assignmentDal;
        }

        internal UploadModel UploadFile(IFormFile file, List<string> contentTypes, int appUserId, int taskId, bool isItUpdate)
        {

            UploadModel uploadModel = new UploadModel();

            Assignment assignment = null;
            
            if (isItUpdate)
            {
                assignment = _assignmentDal.GetSpesificAssignment(appUserId, taskId);
            }

            if (assignment != null && isItUpdate==false)
            {
                uploadModel.ErrorMessage = "Zaten bir ödev yüklenmişsiniz!";
                uploadModel.UploadState = UploadState.Error;
                return uploadModel;
            }


            if (file != null)
            {
                if (!contentTypes.Contains(file.ContentType))
                {
                    uploadModel.ErrorMessage = "Desteklenmeyen Dosya Türü";
                    uploadModel.UploadState = UploadState.Error;
                }
                else
                {
                    var newName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assignments/" + newName);
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyTo(stream);

                    uploadModel.NewName = newName;
                    uploadModel.UploadState = UploadState.Success;
                }

            }
            else
            {

                uploadModel.UploadState = UploadState.NotExist;

                if (isItUpdate)
                {
                    uploadModel.WarningMessage = "Herhangi bir dosya yüklenmedi.";
                }
                else
                {
                    uploadModel.ErrorMessage = "Dosya yüklemek zorundasınız.";
                }
            }

            return uploadModel;
        }
    }
}
