window.createLineChart = function (chartData, elementId, theme, height, width)
{
	var chartElement = document.createElement('div');

	var chart = LightweightCharts.createChart(chartElement, {
		width: width,
		height: height,
		layout: {
			background: {
				type: 'solid',
				color: '#000000',
			},
			textColor: 'rgba(255, 255, 255, 0.9)',
		},
		grid: {
			vertLines: {
				color: 'rgba(197, 203, 206, 0.5)',
			},
			horzLines: {
				color: 'rgba(197, 203, 206, 0.5)',
			},
		},
		crosshair: {
			mode: LightweightCharts.CrosshairMode.Normal,
		},
		rightPriceScale: {
			borderColor: 'rgba(197, 203, 206, 0.8)',
		},
		timeScale: {
			borderColor: 'rgba(197, 203, 206, 0.8)',
		},
	});

	document.getElementById(elementId).appendChild(chartElement);

	var candleSeries = chart.addCandlestickSeries({
		upColor: 'rgba(255, 144, 0, 1)',
		downColor: '#000',
		borderDownColor: 'rgba(255, 144, 0, 1)',
		borderUpColor: 'rgba(255, 144, 0, 1)',
		wickDownColor: 'rgba(255, 144, 0, 1)',
		wickUpColor: 'rgba(255, 144, 0, 1)',
	});

	function convertDate(inputFormat) {
		var dt = new Date(inputFormat);
		return dt.getFullYear() + '-' + (dt.getMonth() + 1) + '-' + dt.getDate();
	}

	var processedData = chartData.map(d => ({
		time: convertDate(d.time),
		open: d.open,
		high: d.high,
		low: d.low,
		close: d.close
	}));

	candleSeries.setData(processedData);

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
		candleSeries.applyOptions(themesData[theme].series);
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
}

