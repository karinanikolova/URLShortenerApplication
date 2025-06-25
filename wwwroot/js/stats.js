window.getTopVisitorsChart = function (labels, data) {
    const ctx = document.querySelector('#topVisitorsChart').getContext('2d');

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Visits Count',
                data: data,
                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Number of Visits'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'IP Address'
                    }
                }
            }
        }
    });
};