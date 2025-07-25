namespace StenglonesApi.Utils;

public static class TemperatureConverter
{
    public static double FahrenheitToCelsius(double fahrenheit) =>
        (fahrenheit - 32) / 1.8;

    public static double CelsiusToFahrenheit(double celsius) =>
        (celsius * 1.8) + 32;
}
