let estudianteId = 0;

addTarea = async (datos) => {
	debugger;
	const settings = {
		method: 'POST',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos)
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/tareas`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
};

getTarea = async (id) => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://lmazfunction.azurewebsites.net/api/tareas/${id}`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
};

getTareas = async (estudianteId) => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
	};
	try {
		const fetchResponse = await fetch('https://lmazfunction.azurewebsites.net/api/tareas?estudianteId=' + estudianteId, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
};

guardarTarea = async () => {
	let datos = {
		"titulo": $('#titulo').val(),
		"fechaDentrega": $('#fecha').val(),
		"descripcion": $('#descripcion').val(),
		"completado": 0,
		"estudianteId": estudianteId,
		"materia": $('#materias').val()
	};

	let result = await addTarea(datos);

	if (result > 0) {
		alert('Tarea agregada');
		$("#modalTarea").modal('toggle');
		$('#nombre').val('');
		$('#descripcion').val('');
		$('#fecha').val('');
		$('#entregada').val('');
		$('#tareas').empty();
		carga();
	} else {
		alert('Error al agregar tarea');
	}
};

carga = async () => {
	estudianteId = window.location.search.split('estudianteId=')[1]
	let tareas = await getTareas(estudianteId);

	for (var i = 0; i < tareas.length; i++) {
		$('#tareas').append('<tr><td>' + tareas[i].Nombre + '</td><td>' + tareas[i].Descripcion + '</td><td>' + tareas[i].FechaEntrega + '</td><td>' + tareas[i].Entregada + '</td></tr>');
	};
};

cargarMaterias = async () => {
	let res = (await getMaterias()).Value;

	for (var i = 0; i < res.length; i++) {
		$('#materias').append('<option value="' + res[i].Id + '">' + res[i].Nombre + '</option>');
	};
};