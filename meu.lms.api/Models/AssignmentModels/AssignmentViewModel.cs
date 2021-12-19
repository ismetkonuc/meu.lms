namespace meu.lms.api.Models.AssignmentModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string FilePath { get; set; }
        public int Score { get; set; }

        public int UserId { get; set; }
    }
}