using System.Text.Json.Serialization;

namespace AreaService;

public class ShapeParameters {

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShapeType TheShapeType { get; set; }
    
    public List<double> Sides { get; set; } = new List<double>();
}