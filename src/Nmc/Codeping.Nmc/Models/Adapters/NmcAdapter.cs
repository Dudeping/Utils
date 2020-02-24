namespace Codeping.Nmc
{
    internal static class NmcAdapter
    {
        public static string ToAddress(this ForecastType type)
        {
            return type switch
            {
                ForecastType.暴雨预警 => Constants.FORECAST_WARNING_DOWNPOUR,
                ForecastType.大雾预警 => Constants.FORECAST_WARNING_FOG,
                ForecastType.每日天气提示 => Constants.FORECAST_WEATHER_PERDAY,
                ForecastType.中期天气预报 => Constants.FORECAST_MIDRANGE_BULLETIN,
                ForecastType.国外天气预报 => Constants.FORECAST_ABROAD_WEATHER,
                ForecastType.环境气象公报 => Constants.FORECAST_ENVIRONMENTAL,
                ForecastType.公路气象预报 => Constants.FORECAST_TRAFFIC,
                ForecastType.强对流天气预报 => Constants.FORECAST_SWPC_BULLETIN,
                ForecastType.森林火险气象预报 => Constants.FORECAST_FORESTFIRE_DOC,
                ForecastType.草原火险气象预报 => Constants.FORECAST_GLASSLAND_FIRE,
                ForecastType.天气公报 => Constants.FORECAST_WEATHER_BULLETIN,
                _ => Constants.FORECAST_WEATHER_BULLETIN,
            };
        }
    }
}
