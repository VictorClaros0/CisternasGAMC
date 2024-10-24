<script>
    // Mostrar el preloader durante 2 segundos antes de cargar la página
    // Mostrar el preloader y ocultarlo después de 2 segundos
    window.addEventListener("load", function() {
        // Retrasa la ocultación del preloader por 2 segundos
        setTimeout(function () {
            const preloader = document.getElementById("preloader");
            if (preloader) {
                preloader.style.display = "none"; // Oculta el preloader
            }
            document.body.classList.add("loaded"); // Añade la clase 'loaded' para indicar que la página ha cargado
        }, 2000); // 2000 ms = 2 segundos
    });
    

    // Animación al hacer scroll: Cargar las tarjetas cuando entren y salgan de la vista
    document.addEventListener("DOMContentLoaded", function () {
        const otbCards = document.querySelectorAll(".otb-card");

        // Configuración del IntersectionObserver para detectar las tarjetas en la vista
        const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Mostrar la tarjeta con animación cuando entra en la vista
                entry.target.classList.add('show');
                entry.target.classList.remove('hide-down');
            } else {
                // Ocultar la tarjeta con animación cuando sale de la vista
                entry.target.classList.remove('show');
                entry.target.classList.add('hide-down');
            }
        });
        }, {
        threshold: 0.2  // Detecta cuando el 20% de la tarjeta entra o sale de la vista
        });

        // Aplica el observador a cada tarjeta OTB
        otbCards.forEach(card => observer.observe(card));
    });

    // Filtrar las tarjetas según el texto del buscador
    function filterOtbs() {
        const input = document.getElementById("searchBox").value.toLowerCase();
    const cards = document.querySelectorAll(".otb-card");

        // Recorre cada tarjeta y filtra en base al título
        cards.forEach(card => {
            const name = card.querySelector(".card-title").innerText.toLowerCase();

    if (name.includes(input)) {
        card.classList.remove('slide-up');
    card.classList.add('slide-down');
    card.parentElement.style.display = "block"; // Muestra la tarjeta
            } else {
        card.classList.remove('slide-down');
    card.classList.add('slide-up');

                // Retrasa la ocultación hasta que la animación termine
                setTimeout(() => {
        card.parentElement.style.display = "none";
                }, 300); // Duración de la animación ajustada para mejor rendimiento
            }
        });
    }

    // Validar la entrada del buscador: no permite números, caracteres especiales ni espacios no deseados
    function validateInput() {
        const searchBox = document.getElementById("searchBox");
    const invalidChars = /[^a-zA-Z\s]/g; // Solo permite letras y espacios

    // Reemplaza caracteres inválidos y ajusta los espacios
    searchBox.value = searchBox.value
    .replace(invalidChars, '')         // Elimina caracteres especiales y números
    .replace(/^\s+/g, '')              // Elimina espacios al inicio
    .replace(/\s{2,}/g, ' ');          // Reemplaza múltiples espacios consecutivos por uno solo
    }

    // Redirige a la página de detalles con el ID de la OTB
    function goToDetails(otbId) {
        window.location.href = `/Citizen/CisternCalendar?SelectedOtb=${otbId}`; // Redirige a la URL con el ID de la OTB seleccionado
    }
</script>
