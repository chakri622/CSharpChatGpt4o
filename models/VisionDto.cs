using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAiApp.models
{
    public class VisionDto
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
        public int max_tokens { get; set; }
    }

    [JsonDerivedType(typeof(ContentA))]
    [JsonDerivedType(typeof(ContentB))]
    public class Content{

    }
     public class ContentA :Content //text
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class ContentB:Content//Image
    {
        public string type { get; set; }
        public ImageUrl image_url { get; set; }
    }

    public class ImageUrl
    {
        public string url { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public List<Content> content { get; set; }
    }

   
}