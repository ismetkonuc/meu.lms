using System;
using System.Security.AccessControl;

namespace meu.lms.api.Models.ArticleModels
{
    public class ArticleListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PostedTime { get; set; }
        public string AppUserFullName { get; set; }
    }
}