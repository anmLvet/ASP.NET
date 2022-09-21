using AreaService.Controllers;
using AreaService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AreaServiceTests;

public class AreaServiceTest
{
    AreaController controller;

    
    public AreaServiceTest()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var factory = serviceProvider.GetService<ILoggerFactory>();

        var logger = factory.CreateLogger<AreaController>();
        
        controller = new AreaController(logger);
    }

    [Fact]
    public void Get_Circle_Area()
    {
        ShapeParameters parameters = new ShapeParameters(){TheShapeType=ShapeType.Circle,Sides={2}};
        var result = controller.GetArea(parameters);
        Assert.IsType<OkObjectResult>(result as OkObjectResult);
        var area = (result as OkObjectResult)!.Value;
        Assert.Equal(12.6,(double)area,1);        
    }

    [Fact]
    public void Circle_Require_Radius()
    {
        ShapeParameters parameters = new ShapeParameters(){TheShapeType=ShapeType.Circle};
        var result = controller.GetArea(parameters);
        
        Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);  
        var error = (result as BadRequestObjectResult)!.Value;
        Assert.Equal("Радиус круга не задан",error.ToString());
    }

    [Fact]
    public void Triangle_Valid_Sides()
    {
        ShapeParameters parameters = new ShapeParameters(){TheShapeType=ShapeType.Triangle,Sides={6,3,1}};
        var result = controller.GetArea(parameters);
        
        Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);  
        var error = (result as BadRequestObjectResult)!.Value;
        Assert.Equal("Не может быть треугольника с такими значениями сторон",error.ToString());        
    }

    [Fact]
    public void Get_Triangle_Area()
    {
        ShapeParameters parameters = new ShapeParameters(){TheShapeType=ShapeType.Triangle,Sides={4,3,5}};
        var result = controller.GetArea(parameters);
        Assert.IsType<OkObjectResult>(result as OkObjectResult);
        var area = (result as OkObjectResult)!.Value;
        Assert.Equal(6,(double)area);        
    }
}