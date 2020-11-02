
using System;


using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{
	partial class Program
	{
		// vous pouvez mettre vos variables globales ici
		static float tetrahedron;
		static float Angle_Rotation;
		static float Position_Raquette_X;
		static float Position_Raquette_Y;
		// position balle -Mohamed-Ilyes
		static float Position_balle_X;
		static float Position_balle_Y;
		static float angle_balle;
		// vitesse de déplacement de la balle -Mohamed-Ilyes
		static float Delta_balle_X = 0.005f;
		static float Delta_balle_Y = 0.005f;
		// Compteur des points -Philippe
		static int Compteur_points = 0;

		static string Le_Message;

		static float[] Rouge = new float[4] { 0.8f, 0.2f, 0.2f, 1 };  // ce tableau représente une couleur composée de 80% de rouge, 20% de Bleu et 20% vert. La dernière valeur doit être 1
		static float[] Bleu = new float[4] { 0.2f, 0.2f, 0.8f, 1 };   // ce tableau représente une couleur composée de 20% de rouge, 20% de Bleu et 80% vert. La dernière valeur doit être 1
		static float[] Vert_Pur;
		//==========================================================
		// Cette fonction est invoquée qu'une seule fois avant que le moteur OpenGl travaille.
		// elle est utile pour initialiser des éléments globaux à l'application
		static void Initialisation_Animation()
		{

			Angle_Rotation = 0;
			Position_Raquette_X = 0;
			Position_Raquette_Y = 0;
			Vert_Pur = new float[4] { 0.0f, 1.0f, 0.0f, 1 }; // vert pur
			Le_Message = "voici un texte";

			// creation d'une position aléatoire pour la balle au depart -Philippe
			Random Generateur_Position_balle;
			Generateur_Position_balle = new Random();
			Position_balle_Y = Generateur_Position_balle.Next(0, 11);
			Position_balle_X = Generateur_Position_balle.Next(0, 15);
			angle_balle = Generateur_Position_balle.Next(0, 1);


		}

		//==========================================================
		// Cette fonction est invoquée par OpenGl chaque fois que l'on demande un glutPostRedisplay();
		static void Afficher_Ma_Scene()
		{
			//.... DEBUT DE NE PAS TOUCHER
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);   // Effacer les buffer d'affichage, de profondeur et de masque
			Gl.glMatrixMode(Gl.GL_MODELVIEW);  // choisir la matrice de vue
			Gl.glLoadIdentity(); // initialiser la matrice vue en matrice identité Le repere est donc en 0,0,0
			Glu.gluLookAt(0.0, 0.0, 20.0, // La caméra est à  0,0,20 (x y z)
									0.0, 0.0, 0.0,   // regarde 0,0,0 (le centre)
									0.0, 1.0, 0.0);  // vecteur orientation  (vers le haut)
													 //..... FIN DE NE PAS TOUCHER
													 // c'est ici que vous pouvez coder l'affichage d'une frame

			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, Rouge); // la couleur est choisie pour tout le reste de l'affichage jusqu'à ce que l'on en change
			Gl.glPushMatrix(); // sauvegarde du repère (on est actuellement en 0,0,0
			Gl.glTranslatef(Position_Raquette_X, Position_Raquette_Y, 0); // déplacer le repère sur l'axe X et L'axe Y. on ne touche pas au Z
			Gl.glRotatef(Angle_Rotation, 0, 1.0f, 0); // faire tourner le repère autour de l'axe vertical

			//SATYA---------------------------------------------------
			Glut.glutWireSphere(0.95f, 50, 20); // afficher la raquette qui va suivre le curseur de la souris, ici une sphere (Wire--HUGO modif) --SATYA
			Gl.glPopMatrix();                   // restitution du repère (on revient donc en 0,0,0)
												//SATYA---------------------------------------------------


			// MODIFICATION DE Philippe 1 et 2 //

			Gl.glPushMatrix();
			Gl.glTranslatef(Position_balle_X, Position_balle_Y, 0); // Balle positionement aléatoire 
			Gl.glRotatef(angle_balle, 0, 1.0f, 0); // rotation de la balle 
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, Vert_Pur); // la couleur de dessin est maintenant vert_pur pour balle (on la repére mieux au moins)
			Glut.glutSolidSphere(0.5f, 20, 20); // modif création d'une balle
			Gl.glPopMatrix();

			// FIN DE MODIFICATION DE Philippe SUR LA VERSION 1 et 2 //

			Gl.glColor3f(0.9f, 0.5f, 0.9f); // choix de la couleur 90% de rouge 50% de vert 90% bleu

			OPENGL_Affiche_Chaine(-2, 10, Le_Message); // on affiche un texte en -10,-100 (cette fonction est développée dans Moteur_OpenGl.cs)
													   //........NE PAS TOUCHER .......................
			Glut.glutSwapBuffers();
		}
		//=========================================================


		// cette fonction est invoquée en boucle par openGl
		static void Animation_Scene()
		{
			// Création du mouvement de la balle en X et Y  VERSION 3 -HUGO

			Position_balle_X = Position_balle_X + Delta_balle_X;


			if (Position_balle_X > 14 || Position_balle_X < -14) // rebond sur les bordures droite et gauche -HUGO
			{
				Delta_balle_X = -Delta_balle_X;

			}

			Position_balle_Y = Position_balle_Y + Delta_balle_Y;

			if (Position_balle_Y > 10 || Position_balle_Y < -10) // rebond sur les bordures hautes et basses -HUGO
			{
				Delta_balle_Y = -Delta_balle_Y;

			}

			angle_balle = angle_balle + 0.1f; // changement du mouvement de la balle sur son angle -HUGO

			// FIN DE MODIFICATION SUR LA VERSION 3 -HUGO

			Angle_Rotation += 0.1f; // on modifie la valeur de l'angle de rotation

			//------------------- Medhy --------------------//

			if (Position_Raquette_X < Position_balle_X + 0.25f &&
				Position_Raquette_X + 0.25f > Position_balle_X &&
				Position_Raquette_Y < Position_balle_Y + 0.25f &&
				Position_Raquette_Y + 0.25f > Position_balle_Y) // On met une marge pour pouvoir attraper la balle avec la raquette
			{

				Compteur_points += 1; // On incrémente le score
				Position_balle_X = 0.0f;
				Position_balle_Y = 0.0f; // Les coordonnées X et Y sont réinitialisé = La balle revient au centre

			}

			Le_Message = $"Score : {Compteur_points}"; // On met à jour le score

			//------------------ FIN  Philippe -------------------//

			//........NE PAS TOUCHER .......................
			Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
		}

		//======================================================================
		// cette fonction est invoquée par OpenGl lorsqu'on appuie sur une touche spéciale (flèches, Fx, ...)
		// P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on appuie sur une touche
		static void Gestion_Touches_Speciales(int P_Touche, int P_X, int P_Y)
		{
			Console.WriteLine($"Touche Spéciale : {P_Touche}. La souris est en {P_X} {P_Y}");



			if (P_Touche == 100)  // 100 est le code de la touche <- (fleche de gauche quoi)
			{
				Position_Raquette_X -= 0.5f;
			}

			if (P_Touche == 102)  // 102 est le code de la touche ->
			{
				Position_Raquette_Y += 0.5f;
			}

			//........NE PAS TOUCHER .......................
			Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

		}

		//======================================================================
		// cette fonction est invoquée par OpenGl lorsqu'on appuie sur une touche normale (A,Z,E, ...)
		// P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on appuie sur une touche
		static void Gestion_Clavier(byte P_Touche, int P_X, int P_Y)
		{
			Console.WriteLine($"Touche Normale : {P_Touche}. La souris est en {P_X} {P_Y}");

			if (P_Touche == 27) // 27 est la touche "Échap"
			{
				Glut.glutLeaveMainLoop();
			}



			Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

		}

		//==================================================================================
		// cette fonction est invoquée par OpenGl lorsqu'on appuie sur un bouton de la souris
		// P_Bouton contient le code du bouton (gauche ou droite), P_Etat son etat, les coordonnées de la souris quand on appuie sur un bouton sont dans P_X et P_Y

		static void Gestion_Bouton_Souris(int P_Bouton, int P_Etat, int P_X, int P_Y)
		{
			Console.WriteLine($"Bouton Souris : {P_Bouton} est {P_Etat}. La souris est en {P_X} {P_Y}");
			//Le_Message = $"Bouton Souris : {P_Bouton} est {P_Etat}. La souris est en {P_X} {P_Y}";


			// Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
		}

		//====================================================================
		// cette fonction est invoquée par OpenGl lorsqu'on tourne la molette de la souris
		// P_Molette contient le code de la molette, P_Sens son sens de rotation, les coordonnées de la souris quand on tourne la molette sont dans P_X et P_Y

		static void Gestion_Molette(int P_Molette, int P_Sens, int P_X, int P_Y)
		{
			Console.WriteLine($"Molette Souris : {P_Molette} tourne dans le sens {P_Sens}. La souris est en {P_X} {P_Y}");
			//Le_Message = $"Molette Souris : {P_Molette} tourne dans le sens {P_Sens}. La souris est en {P_X} {P_Y}";

			// Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
		}

		//====================================================================
		// cette fonction est invoquée par OpenGl lorsqu'on bouge la souris sans appuyer sur un bouton
		// les coordonnées de la souris ont dans P_X et P_Y
		static void Gestion_Souris_Libre(int P_X, int P_Y)
		{
			// On convertit les donnes de la souris au format "OpenGL" (-1;1) --SATYA-----------------------
			float X = ((P_X / 800.0f) * 2.0f - 1.0f);
			float Y = -((P_Y / 600.0f) * 2.0f - 1.0f);
			Position_Raquette_X = X * 15.0f; //la raquette suit le curseur de la souris --SATYA
			Position_Raquette_Y = Y * 15.0f;

			//	Console.WriteLine($"Souris libre en {P_X} {P_Y}");


			Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

		}


		//====================================================================
		// cette fonction est invoquée par OpenGl lorsqu'on bouge la souris tout en appuyant sur un bouton
		// les coordonnées de la souris ont dans P_X et P_Y
		static void Gestion_Souris_Clique(int P_X, int P_Y)
		{
			Le_Message = $"Souris cliqué en {P_X} {P_Y}";

			Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

		}


	}
}
