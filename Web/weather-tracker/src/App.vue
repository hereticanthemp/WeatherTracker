<template>
  <div>
    <img alt="Vue logo" src="./assets/logo.png" />
    <weather-board />
  </div>
</template>

<script>
import WeatherBoard from "@/components/WeatherBoard.vue";
export default {
  name: "App",
  components: {
    WeatherBoard,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
