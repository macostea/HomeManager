window.Charts = {};

window.ChartJsInterop = class {
    static SetupChart(config) {
        const c = JSON.parse(config);

        const ctx = document.getElementById(c.canvasId);
        console.log("Setting chart", c.canvasId, c);

        if (c.canvasId in window.Charts) {
            console.log("Found the chart, updating");
            console.log(c);
            const chart = window.Charts[ctx];
            console.log(chart);
            chart.chart.config.data.datasets = c.data.datasets;
            chart.chart.config.data.labels = c.data.labels;

            chart.chart.render({
                duration: 800,
                lazy: false,
                easing: "easeOutBounce"
            });

            chart.chart.update();
        } else {
            const chart = new Chart(c.canvasId, c);
            window.Charts[ctx] = chart;
        }

        return true;
    }
}
