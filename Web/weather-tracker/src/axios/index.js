import axios from "axios";

var instance = axios.create({
    baseURL:'/'
})

export default {
    GetWeather: ()=>{
        return instance.get('WeatherForecast');
    }    
}