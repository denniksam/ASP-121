// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const signInButton = document.getElementById("signin-button");
    if (signInButton) signInButton.addEventListener('click', signInButtonClick);
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
        "/User/LogIn",          // спосіб надсилання даний від клієнта до сервера
        {                       // без оновлення/руйнування сторінки
            method: "POST",     // "/User/LogIn" - URL - адреса запиту
            body: data          // method - метод запиту (на відміну від форм - довільний)
        })                      // body - тіло запиту, для надсилання форм вживається
        .then(r => r.json())    // спеціальний об'єкт-конструктор форм FormData
        .then(j => {            // Відповідь одержується у два етапи - 
            console.log(j);     // 1.then r => r.json() / r => r.text()
                                // 2.then - робота з json або text
            /*
            Тут ми очікуємо, що j - це один з варіантів: { status = "OK" } або { status = "NO" }
            Відповідно, слід перевірити, що у об'єкта j є поле "status"
            та у разі якщо його значення "ОК", то вхід дозволено, інакше - відхилено.
            Завдання - реалізувати зазначені перевірки, видати повідомлення
            про успішність або помилковість автентифікації (входу до системи)
            Можна у вигляді alert або у вільномі місці модального вікна
            */
        });                     
}
