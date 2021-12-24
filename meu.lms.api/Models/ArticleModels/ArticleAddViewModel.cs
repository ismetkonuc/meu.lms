using System;

namespace meu.lms.api.Models.ArticleModels
{
    public class ArticleAddViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CourseId { get; set; }
    }
}