function createChat (chartData, elementId, theme, height, width) {

	var chartElement = document.createElement('div');

	var chart = LightweightCharts.createChart(chartElement, {
		width: width,
		height: height,
		rightPriceScale: {
			borderVisible: false,
		},
		priceScale: {
			mode: LightweightCharts.PriceScaleMode.Logarithmic,
		},
		timeScale: {
			timeVisible: true,
			secondsVisible: false,
		},
	});

	document.getElementById(elementId).appendChild(chartElement);

	var areaSeries = chart.addAreaSeries({
		topColor: 'rgba(33, 150, 243, 0.56)',
		bottomColor: 'rgba(33, 150, 243, 0.04)',
		lineColor: 'rgba(33, 150, 243, 1)',
		lineWidth: 2,
	});

	var processedData = chartData.map(d => ({ time: d.time, value: d.value }));
    console.log(chartData.map(d => d.time));
	areaSeries.setData(processedData);

	var darkTheme = {
		chart: {
			layout: {
				background: {
					type: 'solid',
					color: '#2B2B43',
				},
				lineColor: '#2B2B43',
				textColor: '#D9D9D9',
			},
			watermark: {
				color: 'rgba(0, 0, 0, 0)',
			},
			crosshair: {
				color: '#758696',
			},
			grid: {
				vertLines: {
					color: '#2B2B43',
				},
				horzLines: {
					color: '#363C4E',
				},
			},
		},
		series: {
			topColor: 'rgba(32, 226, 47, 0.56)',
			bottomColor: 'rgba(32, 226, 47, 0.04)',
			lineColor: 'rgba(32, 226, 47, 1)',
		},
	};

	const lightTheme = {
		chart: {
			layout: {
				background: {
					type: 'solid',
					color: '#F8F7FA',
				},
				lineColor: '#2B2B43',
				textColor: '#191919',
			},
			watermark: {
				color: 'rgba(0, 0, 0, 0)',
			},
			grid: {
				vertLines: {
					visible: false,
				},
				horzLines: {
					color: '#f0f3fa',
				},
			},
		},
		series: {
			topColor: 'rgba(33, 150, 243, 0.56)',
			bottomColor: 'rgba(33, 150, 243, 0.04)',
			lineColor: 'rgba(33, 150, 243, 1)',
		},
	};

	var themesData = {
		Dark: darkTheme,
		Light: lightTheme,
	};

	function syncToTheme(theme) {
		chart.applyOptions(themesData[theme].chart);
		areaSeries.applyOptions(themesData[theme].series);
	}

	syncToTheme(theme);

	const resizeObserver = new ResizeObserver(entries => {
		for (let entry of entries) {
			if (entry.target === chartElement) {
				const newRect = entry.contentRect;
				chart.applyOptions({ height: newRect.height, width: newRect.width });
			}
		}
	});

	resizeObserver.observe(chartElement);
};