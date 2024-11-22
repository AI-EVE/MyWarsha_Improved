using System;
using MyWarsha_DTOs.CarGenerationDtos;
using MyWarsha_DTOs.CarMakerDtos;
using MyWarsha_DTOs.CarModelDTOs;

namespace MyWarsha_DTOs;

public class CarInfoDto
{
    public CarMakerDto CarMaker { get; set; } = null!;
    public CarModelDto CarModel { get; set; } = null!;
    public CarGenerationDto CarGeneration { get; set; } = null!;
}
