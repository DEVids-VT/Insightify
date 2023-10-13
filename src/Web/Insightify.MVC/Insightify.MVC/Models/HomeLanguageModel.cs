namespace Insightify.MVC.Models
{
    public class HomeLanguageModel
    {
        public VideoText VideoText { get; set; }
        public string Subtitle { get; set; }
        public List<ListElement> Ul { get; set; }
        public string CenterText { get; set; }
        public string LogIn { get; set; }
        public string SignUp { get; set; }
        public string Terms { get; set; }
        public string Policy { get; set; }
        public string Language { get; set; }
        public string LanguageProp { get; set; }
        public string LanguageImg { get; set; }
    }

    public class VideoText
    {
        public string Title { get; set; }
        public string Highlight { get; set; }
        public string Subtitle { get; set; }
    }

    public class ListElement
    {
        public string Text { get; set; }
        public string Highlight { get; set; }
        public string Description { get; set; }
    }
}
