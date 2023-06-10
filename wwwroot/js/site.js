// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const signInButton = document.getElementById("signin-button");
    if (signInButton) signInButton.addEventListener('click', signInButtonClick);

    for (let element of document.querySelectorAll("[data-rate-value]")) {
        element.addEventListener('click', rateClick);
    }
});
function signInButtonClick() {
    const userLoginInput = document.getElementById("signin-login");
    const userPasswordInput = document.getElementById("signin-password");
    if (!userLoginInput) throw "Елемент не знайдено: signin-login";
    if (!userPasswordInput) throw "Елемент не знайдено: signin-password";

    const userLogin = userLoginInput.value;
    const userPassword = userPasswordInput.value;
    if (userLogin.length === 0) {
        alert("Введіть логін");
        return;
    }
    if (userPassword.length === 0) {
        alert("Введіть пароль");
        return;
    }
    // console.log(userLogin, userPassword);
    const data = new FormData();
    data.append("login", userLogin);
    data.append("password", userPassword);
    fetch(                      // fetch - AJAX (Async Js And Xml) - асинхронний
        "/User/LogIn",          // спосіб надсилання даних від клієнта до сервера
        {                       // без оновлення/руйнування сторінки
            method: "POST",     // "/User/LogIn" - URL - адреса запиту
            body: data          // method - метод запиту (на відміну від форм - довільний)
        })                      // body - тіло запиту, для надсилання форм вживається
        .then(r => r.json())    // спеціальний об'єкт-конструктор форм FormData
        .then(j => {            // Відповідь одержується у два етапи - 
            console.log(j);     // 1.then r => r.json() / r => r.text()
                                // 2.then - робота з json або text
            if (typeof j.status != 'undefined') {
                if (j.status == 'OK') {
                    window.location.reload();   // оновлюємо сторінку як для автентифікованого користувача
                }
                else {

                }
            }
        });                     
}

function rateClick(e) {  // like / dislike
    // user-id:
    const div = e.target.closest('div');
    const userId = div.getAttribute('data-user-id');
    if (typeof userId == 'undefined' || userId.length == 0) {
        alert("Для оцінювання слід автентифікуватись");
        return;
    }
    const productId = div.getAttribute('data-product-id');
    const rate = e.target.getAttribute('data-rate-value');

    console.log(userId, productId, rate);
    window
        .fetch("/api/rate", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                productId,
                userId,
                'rating': rate
            })
        })
        .then(r => r.json())
        .then(j => {
            console.log(j);
        });
}
