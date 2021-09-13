function RedirectAfterSubscribing() {
    let pathName = window.location.pathname;

    if (pathName == "/Memberships/Success" || pathName == "/Feedbacks/ThankYou") {
        function countdown() {
            var counter = document.getElementById('counter');
            if (parseInt(counter.innerHTML) <= 0) {
                if (pathName == "/Memberships/Success") {
                    window.location.href = 'https://localhost:44319/Dashboard/Index'
                }

                window.location.href = 'https://localhost:44319/Quizzes/All';
            }
            if (parseInt(counter.innerHTML) != 0) {
                counter.innerHTML = parseInt(counter.innerHTML) - 1;
            }
        }
        setInterval(function () { countdown(); }, 1000);
    }
}

RedirectAfterSubscribing();