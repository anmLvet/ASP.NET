using Microsoft.AspNetCore.Mvc;

namespace AreaService.Controllers;

[ApiController]
[Route("[controller]")]
public class AreaController : ControllerBase
{
        private readonly ILogger<AreaController> _logger;

        public AreaController(ILogger<AreaController> logger)
        {

            _logger = logger;
        }


    [HttpPost]
    [ProducesResponseType(200,Type=typeof(double))]
    [ProducesResponseType(400)]
    public IActionResult GetArea([FromBody] ShapeParameters parameters)
    {
        switch (parameters.TheShapeType)
        {
            case ShapeType.Circle:
                if ( parameters.Sides.Count < 1 )
                {
                    return BadRequest("Радиус круга не задан");
                }
                else if (parameters.Sides[0] <= 0)
                {
                    return BadRequest("Радиус круга должен быть положительным");
                }
                else 
                {
                    return Ok(Math.PI*parameters.Sides[0]*parameters.Sides[0]);
                }        
                
            case ShapeType.Triangle:
                if ( parameters.Sides.Count < 3)
                {
                    return BadRequest("Все стороны треугольника должны быть заданы");
                }
                if ( parameters.Sides[0] <= 0 || parameters.Sides[1] <= 0 || parameters.Sides[2] <= 0)
                {
                    return BadRequest("Все стороны треугольника должны быть неотрицательны");
                }
                double maxSide = Math.Max(parameters.Sides[0], Math.Max(parameters.Sides[1],parameters.Sides[2]));
                // самая длинная сторона должна быть короче суммы двух других сторон
                if (parameters.Sides[0] + parameters.Sides[1] + parameters.Sides[2] <= 2*maxSide) 
                {
                    return BadRequest("Не может быть треугольника с такими значениями сторон");
                }
                double halfPerimeter = (parameters.Sides[0] + parameters.Sides[1] + parameters.Sides[2]) / 2.0;

                // формула Герона
                return Ok(Math.Sqrt(halfPerimeter*(halfPerimeter-parameters.Sides[0])*(halfPerimeter-parameters.Sides[1])*(halfPerimeter-parameters.Sides[2])));

            default:
                return BadRequest("Неподдерживаемый вид геометрической фигуры");

        }  

    }

}