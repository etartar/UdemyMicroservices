namespace FreeCourse.Shared.Messages
{
    public class CourseNameChangedEvent
    {
        public CourseNameChangedEvent()
        {
        }

        public CourseNameChangedEvent(string courseId, string updatedName)
        {
            CourseId = courseId;
            UpdatedName = updatedName;
        }

        public string CourseId { get; set; }
        public string UpdatedName { get; set; }
    }
}
