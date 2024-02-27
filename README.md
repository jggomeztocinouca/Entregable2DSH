# COSAS POR HACER

## Lógica y funcionalidad

### Nivel 3

- Modificar la generación de premios para que ahora genere también una valla en el mismo lugar y el premio más arriba, obligando así al jugador a saltar. Assets de valla incluidos en el proyecto.

## Diseño

### Para cada escena

- Música de fondo
- Estilización (colores, imágenes, etc.)

### UI (para cada nivel)

- Contador / Marcador de puntos recogidos en cada nivel
  - Nivel 1 -> Empezar con 0 puntos
  - Nivel 2 -> Empezar con 10 puntos
  - Nivel 3 -> Empezar con 20 puntos

### Pantalla de inicio (No implementada)

- Redirigir a la pantalla de carga 1

### Pantalla de carga 1

- Desactivar botón de Start una vez pulsado
- Poner un png de la tecla de la barra espaciadora indicando que sirve para cambiar entre las direcciones derecha y recto.

### Pantalla de carga 2

- Desactivar botón de Start una vez pulsado
- Poner un png de las teclas AWSD indicando que sirve para cambiar entre las direcciones derecha, izquierda, arriba y abajo.

### Pantalla de carga 3

- Desactivar botón de Start una vez pulsado
- Poner un png de las teclas AWSD y la barra espaciadora indicando que sirve para cambiar entre las direcciones derecha, izquierda, arriba, abajo y recto y saltar, respectivamente.

### Nivel 1

- Cuando y < -10 -> Reproducir sonido de caída
- Cuando y < -20 -> Pantalla de carga 1 (Comprobar) [Si no está hecho, simplemente cambiar Level1 por PreLevel1]
- Cuando coge un premio -> Reproducir sonido

### Nivel 2

- Cuando y < -10 -> Reproducir sonido de caída
- Cuando y < -20 -> Pantalla de carga 2 (Comprobar)
- Cuando coge un premio -> Reproducir sonido

### Nivel 3

- Cuando y < -10 -> Reproducir sonido de caída
- Cuando y < -20 -> Pantalla de carga 3 (Comprobar)
- Cuando coge un premio -> Reproducir sonido
