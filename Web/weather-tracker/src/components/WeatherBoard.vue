<template>
  <div>
    <h1>Weather Board</h1>
    <div>{{ start }} ~ {{ end }}</div>
    <button @click="refresh">refresh</button>
    <table>
      <thead>
        <th>Location</th>
        <th>Weather</th>
        <th>Comfort</th>
        <th>Temperature</th>
      </thead>
      <tbody>
        <div v-if="weathers == null">No Data</div>
        <tr v-for="w in weathers" :key="w.locationName">
          <td>{{ w.locationName }}</td>
          <td>{{ w.weather }}</td>
          <td>{{ w.comfort }}</td>
          <td>{{ w.minTemperature }}~{{ w.maxTemperature }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import api from "@/axios";
export default {
  data: function () {
    return {
      weathers: null,
    };
  },
  computed: {
    start: function () {
      return new Date(this.weathers[0].startTime).toLocaleString();
    },
    end: function () {
      return new Date(this.weathers[0].endTime).toLocaleString();
    },
  },
  created: function () {
    this.refresh();
  },
  mounted: function () {},
  methods: {
    refresh: async function () {
      let resp = await api.GetWeather();
      this.weathers = resp.data;
    },
  },
};
</script>

<style></style>
