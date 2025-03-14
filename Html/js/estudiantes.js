/* ****************************************************************************************
*                                       Locals
**************************************************************************************** */
let estudianteId = 0;

/* ****************************************************************************************
*                                    API Requests
**************************************************************************************** */
getEstudiantes = async () => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/estudiantes`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

getEstudiante = async (id) => {
	const settings = {
		method: 'GET',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		}
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/estudiantes/${id}`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}

updateEstudiante = async (estudianteId, datos) => {
	const settings = {
		method: 'PUT',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
		body: JSON.stringify(datos)
	};
	try {
		const fetchResponse = await fetch(`https://sdgstefunction.azurewebsites.net/api/estudiantes/${estudianteId}`, settings);
		const data = await fetchResponse.json();
		return data;
	} catch (e) {
		return e;
	}
}


/* ****************************************************************************************
*                                    JS Functions
**************************************************************************************** */
verEstudiante = async () => {
	estudianteId = window.location.search.split('estudianteId=')[1];

	if (estudianteId == 0) {
		$('#nombreEstudiante').html('John Doe');
		$('#correoEstudiante').html('somemail@example.com');
	}
	else {
		let datos = (await getEstudiante(estudianteId)).Value;
		$('#nombreEstudiante').html(datos.Nombre);
		$('#correoEstudiante').html(datos.Correo);
	}
}

editarAlumno = async () => {
	$("#modalEditar").modal('toggle');

	if (estudianteId != 0) {
		let datos = (await getEstudiante(estudianteId)).Value;
		$('#nombreEditar').val(datos.Nombre);
		$('#correoEditar').val(datos.Correo);
		$('#passEditar').val(datos.Password);
	}
}

actualizarDatos = async () => {
	$("#btnEditar").attr("disabled", true);
	let datos = {
		"Nombre": $('#nombreEditar').val(),
		"Correo": $('#correoEditar').val(),
		"Password": $('#passEditar').val()
	};

	let result = (await updateEstudiante(estudianteId, datos)).Value;

	if (result > 0) {
		alert('Datos actualizados');
		$("#modalEditar").modal('toggle');
		$('#nombreEstudiante').html(datos.nombre);
		$('#correoEstudiante').html(datos.correo);
	} else {
		alert('Error al actualizar datos');
	}
	$("#btnEditar").attr("disabled", false);
}