module.exports = {
    devServer: {
        proxy: {
            '/WeatherForecast':{
                target:'https://localhost:5001',
                changeOrigin:true,                
            }
        }
    } }