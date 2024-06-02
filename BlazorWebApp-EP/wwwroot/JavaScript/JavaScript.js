document.addEventListener('DOMContentLoaded', function () {
    var dropdownToggle = document.querySelector('.dropdown-toggle');
    var dropdownMenu = document.querySelector('.dropdown-menu');

    // Handler para clicar no dropdown
    function toggleDropdown(event) {
        dropdownToggle.classList.toggle('active');
        event.stopPropagation(); // Impede que o clique se propague para o documento
    }

    // Handler para clicar fora do dropdown
    function closeDropdown(event) {
        if (!dropdownToggle.contains(event.target) && !dropdownMenu.contains(event.target)) {
            dropdownToggle.classList.remove('active');
        }
    }

    // Adiciona os listeners
    dropdownToggle.addEventListener('click', toggleDropdown);
    document.addEventListener('click', closeDropdown);
});