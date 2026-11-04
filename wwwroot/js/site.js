async function cambiarEstadoTarea(id, nuevoEstado) {
    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');

    if (!tokenInput) {
        console.error("Error: Falta el token de verificación CSRF en el DOM.");
        return;
    }

    const token = tokenInput.value;

    const formData = new FormData();
    formData.append('id', id);
    formData.append('nuevoEstado', nuevoEstado);

    
    try {
        const response = await fetch('?Handler=CambiarEstado', {
            method: 'POST',
            body: formData,
            headers: {
                'RequestVerificationToken': token
            }
        });

        if (!response.ok) {
            throw new Error(`Error HTTP:${response.status}`);
        }

        const data = await response.json();

        if (!data.success) {
            alert("El servidor no pudo actualizar la tarea en la base de datos.");
            return;
        }

        const tarjeta = document.getElementById(`tarea-${id}`);

        if (!tarjeta) {
            console.error(`Error: No se encontro la tarjeta con ID: tarea-${id}`);
            return;
        }

        const columnaId = `columna-${nuevoEstado.replace(/\s+/g, '-')}`;
        const columnaDestino = document.getElementById(columnaId);

        if (!columnaDestino) {
            console.error(`Error: No se encontro la columna destino con ID: ${columnaId}`);
            return;
        }

        const grupoDestino = columnaDestino.querySelector('.grupo-tarjetas');
        if (!grupoDestino) {
            console.error(`Error: La columna ${columnaId} no tiene el contenedor de tarjetas.`);
            return;
        }

        tarjeta.setAttribute('data-estado', nuevoEstado);
        grupoDestino.appendChild(tarjeta);

        tarjeta.classList.add('resaltado');
        tarjeta.scrollIntoView({behavior: 'smooth', block: 'center'});

        setTimeout(() => tarjeta.classList.remove('resaltado'), 1500);
    } catch(error) {
        console.error("Error en la petición asíncrona", error);
        alert("Ocurrió un error de red al intentar cambiar el estado.")
    }
}