function solution() {

    const baseUrl = 'https://localhost:7193/';
    const form = document.querySelector('#urlForm');

    form.addEventListener('submit', formHandler);

    function formHandler(e) {
        e.preventDefault();

        const urlInput = e.target.querySelector('input[name="Url"]').value;

        if (urlInput === '') {
            return;
        }

        const $form = $(form);

        if (!$form.valid()) {
            return;
        }

        const bodyString = 'Url=' + encodeURIComponent(urlInput);

        fetch(form.action, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: bodyString
        })
            .then(response => response.json())
            .then(data => {
                const originalUrl = document.querySelector('#originalUrl');
                originalUrl.textContent = data.originalUrl;
                originalUrl.href = data.originalUrl;

                const shortenedUrl = document.querySelector('#shortenedUrl');
                shortenedUrl.textContent = baseUrl + data.shortenedUrl;
                shortenedUrl.href = baseUrl + data.shortenedUrl;

                document.querySelector('#result').style.display = 'block';

                form.reset();
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
}

$(function () {
    solution();
});