// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    // Función para obtener el total de la compra desde el DOM
    function getCartTotal() {
        const totalText = document.getElementById('cart-total-price').innerText;
    // Extrae solo el número, eliminando el texto "MXN"
    const totalAmount = parseFloat(totalText.replace('MXN', '').trim());
    return totalAmount;
    }

    // Configuración del botón de PayPal
    paypal.Buttons({
        createOrder: function(data, actions) {
            const total = getCartTotal(); // Obtiene el total del carrito
    return actions.order.create({
        purchase_units: [{
        amount: {
        value: total.toFixed(2) // El total debe estar con 2 decimales
                    }
                }]
            });
        },
    onApprove: function(data, actions) {
            return actions.order.capture().then(function(details) {
        alert('Pago realizado con éxito. ¡Gracias por tu compra!');
                // Aquí puedes redirigir a una página de éxito o procesar más acciones
            });
        },
    onError: function(err) {
        console.error(err);
    alert('Ocurrió un error al procesar el pago. Intenta nuevamente.');
        }
    }).render('#paypal-button-container'); // Renderiza el botón de PayPal en el contenedor
</script>