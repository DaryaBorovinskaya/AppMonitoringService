using AppMonitoringService.API.Models;
using AppMonitoringService.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppMonitoringService.API.Controllers
{
    /// <summary>
    /// Контроллер для DeviceData (и DeviceAppData)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        /// <summary>
        /// Сервис логирования
        /// </summary>
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(IDeviceService deviceService,
            ILogger<DeviceController> logger)
        {
            _deviceService = deviceService;
            _logger = logger;
        }

        /// <summary>
        /// Добавление данных (HTTP Post)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] DeviceAppData data)
        {
            try
            {
                _logger.LogInformation("Добавление записи: {deviceId}", data.Id);
                _deviceService.AddData(data);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении устройства");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Получение всех данных в формате DeviceData (HTTP Get)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllDevices()
        {
            try
            {
                _logger.LogInformation("Запрос всех устройств");
                List<DeviceData> devices =  _deviceService.GetAllDevices()
                    .AsEnumerable()
                    .GroupBy(d => d.Id)
                    .Select(g => new DeviceData
                    {
                        Id = g.Key,
                        Name = g.First().Name,
                        Version = g.First().Version,
                        Sessions = g.Select(s => new DeviceSession
                        {
                            StartTime = s.StartTime,
                            EndTime = s.EndTime
                        }).OrderByDescending(s => s.StartTime).ToList()
                    })
                    .ToList(); 
                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка устройств");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Получение данных о конкретном устройстве в формате DeviceData (HTTP Get)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDeviceSessions(string id)
        {
            try
            {
                DeviceData? device = _deviceService.GetDeviceSessions(id)
                    .AsEnumerable()
                    .GroupBy(d => d.Id)
                    .Select(g => new DeviceData
                    {
                        Id = g.Key,
                        Name = g.First().Name,
                        Version = g.Last().Version,
                        Sessions = g.Select(s => new DeviceSession
                        {
                            StartTime = s.StartTime,
                            EndTime = s.EndTime
                        }).OrderByDescending(s => s.StartTime).ToList()
                    })
                    .ToList().FirstOrDefault();

                _logger.LogInformation("Запрошены все записи об устройстве {deviceId}", device.Id);
                return device != null ? Ok(device) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении сессий устройства");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Удаление старых записей о сессиях активности конкретного устройства (HTTP Delete)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpDelete("{id}/old")]  
        public IActionResult DeleteOldRecords([FromRoute] string id,  
                                              [FromQuery] DateTime date) 
        {
            try
            {
                _logger.LogInformation("Удаление записей старше {Date}", date);
                int countDelete = _deviceService.DeleteOldRecords(id,date);
                return Ok(new { CountDelete = countDelete });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении старых записей");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Бэкап данных в файл
        /// </summary>
        /// <returns></returns>
        [HttpGet("backup")]
        public IActionResult Backup()
        {
            _deviceService.Backup();
            return Ok();
        }
    }
}
