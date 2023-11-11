// Function to initialize chat box interactions
function initializeChatBox() {
    var chatBoxHide = document.getElementById('chat-box-hide');
    var chatBox = document.getElementById('chat-box');
    var patrik = document.getElementById('patrik');

    chatBoxHide.addEventListener('click', function () {
        chatBox.classList.add('hidden');
    });
    patrik.addEventListener('click', function () {
        chatBox.classList.remove('hidden');
    });
}

// Function to handle navigation bar show/hide behavior
function initializeNavbar() {
    var btnshow = document.getElementById('navbar-show');
    var btnhide = document.getElementById('navbar-hide');
    var navbar = document.getElementById('navbar');

    btnshow.addEventListener('click', function () {
        if (window.innerWidth <= 780) {
            navbar.classList.remove('hidden');
            navbar.classList.add('fixed-nav');
            page.style.filter = 'blur(2px)';
            btnhide.classList.remove('hidden');
        }
    });

    btnhide.addEventListener('click', function () {
        if (window.innerWidth <= 780) {
            navbar.classList.add('hidden');
            navbar.classList.remove('fixed-nav');
            page.style.filter = 'none';
            btnhide.classList.add('hidden');
        }
    });
}

// Function to initialize profile menu interactions
function initializeProfileMenu() {
    var profilebtn = document.getElementById('img-pfp-btn');
    var profilebtnHide = document.getElementById('pf-menu-hide');
    var pfmenu = document.getElementById('pf-menu');

    let isShown = false;

    profilebtn.addEventListener('click', function () {
        if (isShown) {
            pfmenu.classList.add('hidden');
            isShown = false;
        } else {
            pfmenu.classList.remove('hidden');
            isShown = true;
        }
    });

    profilebtnHide.addEventListener('click', function () {
        pfmenu.classList.add('hidden');
        isShown = false;
    });
}

// Function to initialize notification menu interactions
function initializeNotificationsMenu() {
    var notificationsMenu = document.getElementById('notifications-menu');
    var notificationsMenuHide = document.getElementById('notifications-menu-hide');
    var notificationsBtn = document.getElementById('notifications-btn');

    let isShown = false;

    notificationsBtn.addEventListener('click', function () {
        if (isShown) {
            notificationsMenu.classList.add('hidden');
            isShown = false;
        } else {
            notificationsMenu.classList.remove('hidden');
            isShown = true;
        }
    });

    notificationsMenuHide.addEventListener('click', function () {
        notificationsMenu.classList.add('hidden');
        isShown = false;
    });
}

// Function to handle window resize behavior
function handleWindowResize() {
    window.addEventListener('resize', function () {
        checkSize();
        adjustProfileAndNotificationsMenuVisibility();
    });
}

// Function to check window size and adjust UI accordingly
function checkSize() {
    var navbar = document.getElementById('navbar');
    var btnhide = document.getElementById('navbar-hide');
    var btnshow = document.getElementById('navbar-show');
    var page = document.getElementById('page');

    if (window.innerWidth > 780) {
        navbar.classList.remove('hidden');
        navbar.classList.remove('fixed-nav');
        page.style.filter = 'none';
        btnhide.classList.add('hidden');
        btnshow.classList.add('hidden');
    }
    else {
        btnshow.classList.remove('hidden');
    }
}

// Function to adjust visibility of profile and notifications menu on resize
function adjustProfileAndNotificationsMenuVisibility() {
    var profilebtnHide = document.getElementById('pf-menu-hide');
    var notificationsMenuHide = document.getElementById('notifications-menu-hide');

    if (window.innerWidth <= 420) {
        profilebtnHide.classList.remove('hidden');
        notificationsMenuHide.classList.remove('hidden');
    }
    else {
        profilebtnHide.classList.add('hidden');
        notificationsMenuHide.classList.add('hidden');
    }
}

// Main function to initialize all functionalities
function initialize() {
    initializeChatBox();
    initializeNavbar();
    initializeProfileMenu();
    initializeNotificationsMenu();
    handleWindowResize();
    checkSize();
}

// Event listener for DOMContentLoaded
document.addEventListener('DOMContentLoaded', initialize);
