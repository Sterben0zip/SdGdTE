navegar = (destino) => {
	switch (destino) {
		case 'inicio':
			window.location.href = 'estudiantes.html?estudianteId=' + estudianteId;
			break;
		case 'tareas':
			window.location.href = 'tareas.html?estudianteId=' + estudianteId;
			break;
		case 'salir':
			window.location.href = 'index.html';
			estudianteId = 0;
			break;
	}
}