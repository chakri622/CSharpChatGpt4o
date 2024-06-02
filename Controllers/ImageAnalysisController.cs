using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OpenAiApp.models;
using RestSharp;

namespace OpenAiApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageAnalysisController : ControllerBase
{
    
    private readonly ILogger<ImageAnalysisController> _logger;

    public ImageAnalysisController(ILogger<ImageAnalysisController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetImageData")]
    public async Task<ActionResult> Get()
    {
        var key="sk-proj-aH0GSoCYgG16MhDi9qOrT3BlbkFJuwp2kxwmcgrxf7OjMtc";
        var options = new RestClientOptions("https://api.openai.com/v1/chat/completions");
        var client = new RestClient(options);
        client.AddDefaultHeader("Content-Type", "application/json");
        client.AddDefaultHeader("Authorization",$"Bearer {key}");

        var requestDto =new VisionDto(){
            model ="gpt-4o",
            max_tokens=300,
            messages=new List<Message>(){
                new Message(){
                    role="user",
                    content=new List<Content>(){
                        new ContentA(){
                            type="text",
                            text="What is in this image?"
                        },
                        new ContentB(){
                            type="image_url",
                            image_url=new  ImageUrl(){ url="https://www.thedrive.com/uploads/2023/09/25/GettyImages-88085102.jpg"
                            }
                        }
                    }
                }
            }
        };

        var jsonOption = new JsonSerializerOptions(){
            WriteIndented=true
        };

        var data = JsonSerializer.Serialize(requestDto, jsonOption);

        var request =new RestRequest();
        request.AddJsonBody(data);
        try{
        var response = await client.PostAsync(request);
        
        return Ok(response);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);

        }

        
    }
}
