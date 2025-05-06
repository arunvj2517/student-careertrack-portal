// Mobile Hamburger Menu Toggle Script
document.addEventListener('DOMContentLoaded', function () {
    const hamburger = document.querySelector('.hamburger-menu');
    const headerNav = document.querySelector('.header-nav');

    if (hamburger && headerNav) {
        hamburger.addEventListener('click', function () {
            headerNav.classList.toggle('mobile-open');
            this.setAttribute('aria-expanded',
                this.getAttribute('aria-expanded') === 'true' ? 'false' : 'true');
        });
    }
});
