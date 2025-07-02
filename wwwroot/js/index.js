const form = document.querySelector('#urlForm');

function solution() {

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
            .then(async response => {
                if (!response.ok) {
                    const errorData = await response.json();

                    throw new Error(errorData.error || "Try again.");
                }

                return response.json();
            })
            .then(data => {
                const originalUrl = document.querySelector('#originalUrl');

                originalUrl.textContent = "";
                originalUrl.href = "";

                originalUrl.textContent = data.originalUrl;
                originalUrl.href = data.originalUrl;

                const shortenedUrl = document.querySelector('#shortenedUrl');

                shortenedUrl.textContent = "";
                shortenedUrl.href = "";

                shortenedUrl.textContent = form.action + data.shortenedUrl;
                shortenedUrl.href = form.action + data.shortenedUrl;

                const secretUrl = document.querySelector('#secretUrl');

                secretUrl.textContent = "";
                secretUrl.href = "";

                secretUrl.textContent = form.action + data.secretUrl;
                secretUrl.href = form.action + data.secretUrl;

                document.querySelector('#result').style.display = 'block';

                form.reset();
            })
            .catch(error => {
                console.error('Error:', error.message);
                alert(error.message);
            });
    }
}

$(function () {
    solution();
});