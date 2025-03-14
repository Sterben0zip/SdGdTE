/* ****************************************************************************************
*                                       Locals
**************************************************************************************** */
let estudianteId = 0;
let completado = false;

/* ****************************************************************************************
*                                    API Requests
**************************************************************************************** */
addTarea = async (datos) => {
	const settings = {
		method: 'POST',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos)
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/tareas`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
};

updateTarea = async (id, datos) => {
	const settings = {
		method: 'PUT',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos)
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/tareas/${id}`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

getTarea = async (id) => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/tareas/${id}`, settings);
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
		const fetchResponse = await fetch('https://sdgstefunction.azurewebsites.net/api/tareas?estudianteId=' + estudianteId, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
};

deleteTarea = async (id) => {
	const settings = {
		method: 'DELETE',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/tareas/${id}`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
};

/* ****************************************************************************************
*                                    JS Functions
**************************************************************************************** */
guardarTarea = async () => {
	$("#spinn").attr('hidden', false);
	$("#btnCrear").attr("disabled", true);
	
	let datos = {
		"titulo": $('#titulo').val(),
		"fechaEntrega": $('#fecha').val(),
		"descripcion": $('#descripcion').val(),
		"completado": 0,
		"estudianteId": estudianteId,
		"materiaId": $('#materias').val()
	};

	let result = (await addTarea(datos)).Value;

	if (result > 0) {
		alert('Tarea agregada');
		$("#modalCrear").modal('toggle');
		$('#nombre').val('');
		$('#descripcion').val('');
		$('#fecha').val('');
		$('#entregada').val('');
		$('#tareas').empty();
		carga();
	} else {
		alert('Error al agregar tarea');
	}

	$("#btnCrear").attr("disabled", false);
	$("#spinn").attr('hidden', true);
};

carga = async () => {
	$("#spinn").attr('hidden', false);
	estudianteId = window.location.search.split('estudianteId=')[1]
	let tareas = (await getTareas(estudianteId)).Value;

	for (var i = 0; i < tareas.length; i++) {
		$('#tareas').append('<tr><td>' + tareas[i].Titulo + '</td><td>' + tareas[i].FechaEntrega.substring(0,10) + '</td><td>' + (tareas[i].Completado ? "Completado" : "No completado") + '</td><td>' + tareas[i].Materia.Nombre + '</td><td> <button class="btn btn-sm btn-outline-secondary" data-bs-toggle="modal" data-bs-target="#modalEditar" onclick="editar('+ tareas[i].Id +')">Editar</button> | <button class="btn btn-sm btn-outline-danger" data-bs-toggle="modal" data-bs-target="#modalEliminar" onclick="eliminar('+ tareas[i].Id +')">Eliminar</button> </td> </tr>');
	};
	await cargarMaterias();
	$("#spinn").attr('hidden', true);
};

cargarMaterias = async () => {
	let res = (await getMaterias()).Value;
	$('#materias').empty();
	$('#materiasEditar').empty();

	for (var i = 0; i < res.length; i++) {
		$('#materias').append('<option value="' + res[i].Id + '">' + res[i].Nombre + '</option>');
		$('#materiasEditar').append('<option value="' + res[i].Id + '">' + res[i].Nombre + '</option>');
	};
};

editar = async (id) => {
	let tarea = (await getTarea(id)).Value;

	$('#idEditar').val(tarea.Id);
	$('#tituloEditar').val(tarea.Titulo);
	$('#fechaEditar').val(tarea.FechaEntrega.substring(0,10));
	$('#descripcionEditar').val(tarea.Descripcion);
	completado = tarea.Completado;
	$('#completadoEditar').attr("checked", completado);
	$('#materiasEditar').val(tarea.MateriaId);
}

actualizarTarea = async () => {
	$("#spinn").attr('hidden', false);
	$("#btnActualizar").attr("disabled", true);
	
	let datos = {
		"Titulo": $('#tituloEditar').val(),
		"FechaEntrega": $('#fechaEditar').val(),
		"Descripcion": $('#descripcionEditar').val(),
		"Completado": completado,
		"EstudianteId": estudianteId,
		"MateriaId": $('#materiasEditar').val()
	};

	let result = (await updateTarea($('#idEditar').val(), datos)).Value;

	if (result > 0) {
		alert('Tarea actualizada');
		$("#modalEditar").modal('toggle');
		$('#nombre').val('');
		$('#descripcion').val('');
		$('#fecha').val('');
		$('#entregada').val('');
		$('#tareas').empty();
		carga();
	} else {
		alert('Error al actualizar tarea');
	}

	$("#btnActualizar").attr("disabled", false);
	$("#spinn").attr('hidden', true);
}

eliminar = async (id) => {
	$("#spinn").attr('hidden', false);
	await editar(id);
	$('#idEditar').attr("disabled", true);
	$('#tituloEditar').attr("disabled", true);
	$('#fechaEditar').attr("disabled", true);
	$('#descripcionEditar').attr("disabled", true);
	$('#completadoEditar').attr("disabled", true);
	$('#materiasEditar').attr("disabled", true);
	$("#btnActualizar").attr("hidden", true);
	$("#btnEliminar").attr("hidden", false);
	$("#modalEditar").modal('toggle');
	$("#spinn").attr('hidden', true);
}

confirmarEliminar = async () => {
	$("#spinn").attr('hidden', false);
	$("#btnEliminar").attr("disabled", true);
	let result = (await deleteTarea($('#idEditar').val())).Value;

	if (result > 0) {
		alert('Tarea eliminada');

		$("#modalEditar").modal('toggle');
		$('#tituloEditar').val('');
		$('#fechaEditar').val('');
		$('#descripcionEditar').val('');
		$('#completadoEditar').val('');
		$('#materiasEditar').empty();
		$('#tareas').empty();
		carga();
	}
	else {
		alert('Ocurrio un error al eliminar tarea');
	}

	$("#btnEliminar").attr("disabled", false);
	$("#spinn").attr('hidden', true);
}

toggleCheck = (ipt) => {
	completado = ipt.checked;
}