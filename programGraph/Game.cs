﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace programGraph
{
    internal class Game : GameWindow
    {
        private Escenario escenario;
        private float angle = 0.0f;
        private bool isMouseDown = false;
        private Vector2 lastMousePos;
        private float pitch = 0.0f;
        private float yaw = 0.0f;
        private float zoom = 2.0f;

        public Game(int width, int height)
              : base(width, height, GraphicsMode.Default, "OpenTK Window")
        {
            VSync = VSyncMode.On; // Habilitar VSync para evitar el tearing
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                   MathHelper.DegreesToRadians(45.0f),
                   Width / (float)Height,
                   0.1f,
                   100.0f
               );
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            List<Punto> puntosU = new List<Punto>
                {
                    new Punto(-1, 1,  0.5f), // 0
                    new Punto( 1, 1,  0.5f), // 1
                    new Punto( 1, -1, 0.5f), // 2
                    new Punto(-1, -1, 0.5f), // 3
                    new Punto(-1, 1, -0.5f), // 4
                    new Punto( 1, 1, -0.5f), // 5
                    new Punto( 1, -1, -0.5f), // 6
                    new Punto(-1, -1, -0.5f), // 7
                    new Punto(-0.7f, 1,  0.5f), // 8
                    new Punto( 0.7f, 1,  0.5f), // 9
                    new Punto( 0.7f, -0.7f, 0.5f), // 10
                    new Punto(-0.7f, -0.7f, 0.5f), // 11
                    new Punto(-0.7f, 1, -0.5f), // 12
                    new Punto( 0.7f, 1, -0.5f), // 13
                    new Punto( 0.7f, -0.7f, -0.5f), // 14
                    new Punto(-0.7f, -0.7f, -0.5f)  // 15
                };

            List<Poligono> poligonosU = new List<Poligono>
                {
                    new Poligono(new List<Punto> { puntosU[4], puntosU[12], puntosU[15], puntosU[7] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[7], puntosU[15], puntosU[14], puntosU[6] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[13], puntosU[5], puntosU[6], puntosU[14] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[0], puntosU[8], puntosU[11], puntosU[3] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[3], puntosU[11], puntosU[10], puntosU[2] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[9], puntosU[1], puntosU[2], puntosU[10] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[0], puntosU[4], puntosU[7], puntosU[3] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[8], puntosU[12], puntosU[15], puntosU[11] }, 1.5f, 0.5f, 0.5f),                   
                    new Poligono(new List<Punto> { puntosU[1], puntosU[5], puntosU[6], puntosU[2] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[9], puntosU[13], puntosU[14], puntosU[10] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[3], puntosU[7], puntosU[6], puntosU[2] }, 1.5f, 0.5f, 0.5f),
                    new Poligono(new List<Punto> { puntosU[11], puntosU[15], puntosU[14], puntosU[10] }, 1.0f, 0.0f, 1.0f),
                    new Poligono(new List<Punto> { puntosU[0], puntosU[8], puntosU[12], puntosU[4] }, 0.0f, 1.0f, 1.0f),
                    new Poligono(new List<Punto> { puntosU[9], puntosU[1], puntosU[5], puntosU[13] }, 0.0f, 1.0f, 1.0f)
                };


            Parte partesU = new Parte();
            foreach (var poligono in poligonosU)
            {
                partesU.AddPoligono(poligono);
            }

            Objeto objetoU = new Objeto();
            Objeto objetoU2 = new Objeto();
            Objeto objetoU3 = new Objeto();

            objetoU.Addparte(partesU);
            objetoU2.Addparte(partesU);
            objetoU3.Addparte(partesU);

            objetoU.setCentro(new Punto(2, 2, 2));
            objetoU2.setCentro(new Punto(-1, 2, 3));
            objetoU3.setCentro(new Punto(-1, -2, 2));

            escenario = new Escenario();
            escenario.AddObjeto("U1", objetoU);
            escenario.AddObjeto("U2", objetoU2);
            //escenario.AddObjeto("U3", objetoU3);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Establecer la matriz de modelo/vista
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Aplicar la traslación para el zoom
            GL.Translate(0.0f, 0.0f, -zoom);

            GL.Rotate(pitch, 1.0f, 0.0f, 0.0f); // Rotar alrededor del eje X
            GL.Rotate(yaw, 0.0f, 1.0f, 0.0f);   // Rotar alrededor del eje Y

            // Dibujar los ejes cartesianos
            GL.Begin(PrimitiveType.Lines);

            // Eje X en rojo
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(1.0f, 0.0f, 0.0f);

            // Eje Y en verde
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(0.0f, 1.0f, 0.0f);

            // Eje Z en azul
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(0.0f, 0.0f, 1.0f);

            GL.End();

            // Dibujar el escenario (y por ende el cubo)
            escenario.DibujarEscenario();
            SwapBuffers();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButton.Left)
            {
                isMouseDown = true;
                lastMousePos = new Vector2(e.X, e.Y);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButton.Left)
            {
                isMouseDown = false;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            // Ajustar la distancia de la cámara (zoom) en función del scroll del mouse
            zoom -= e.DeltaPrecise * 0.5f;

            // Limitar el zoom para evitar que pase a través del objeto o se aleje demasiado
            if (zoom < 1.0f)
                zoom = 1.0f;
            if (zoom > 20.0f)
                zoom = 20.0f;
        }


        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            if (isMouseDown)
            {
                Vector2 delta = new Vector2(e.X, e.Y) - lastMousePos;
                lastMousePos = new Vector2(e.X, e.Y);

                yaw += delta.X * 0.5f;  // Ajustar sensibilidad
                pitch += delta.Y * 0.5f; // Invertir si quieres mover el mouse hacia abajo para mover la cámara hacia arriba
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height); // Configurar el viewport al tamaño de la ventana
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            // Lógica de actualización, como manejar la entrada del usuario, se coloca aquí
        }
    }
}
