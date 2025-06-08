namespace Sticky.Models.ViewModels
{
    public class CreateToDoListViewModel
    {
        public string ListName { get; set; }
        public List<string> TaskDescriptions { get; set; }
        public List<ToDoListDto> ToDoLists { get; set; } = new List<ToDoListDto>();
    }

    public class ToDoListDto
    {
        public string Name { get; set; }
        public List<ToDoTask> Tasks { get; set; }
    }
}
