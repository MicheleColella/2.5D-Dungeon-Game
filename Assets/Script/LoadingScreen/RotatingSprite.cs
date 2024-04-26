using UnityEngine;
using UnityEngine.UI; // Importa il namespace UI

public class RotatingSprite : MonoBehaviour
{
    private float rotationSpeed = 90.0f; // Velocità di rotazione in gradi al secondo

    private bool shouldDisappear = false; // La condizione da verificare per far scomparire l'immagine UI
    public Image fadeOutImage; // Riferimento all'immagine da far effettuare il fade-out

    private void Update()
    {
        // Controlla se l'immagine deve scomparire
        if (shouldDisappear)
        {
            // Fai scomparire l'immagine impostando l'alpha del colore a zero
            Image image = GetComponent<Image>();
            Color imageColor = image.color;
            imageColor.a = 0.0f; // Imposta l'alpha a zero per farlo scomparire
            image.color = imageColor;

            // Inizia il fade-out dell'immagine specificata
            if (fadeOutImage != null)
            {
                Color fadeOutColor = fadeOutImage.color;
                fadeOutColor.a -= Time.deltaTime; // Riduci l'alpha per il fade-out
                fadeOutImage.color = fadeOutColor;

                // Quando l'alpha raggiunge zero, l'immagine è completamente trasparente, puoi disattivarla o distruggerla
                if (fadeOutColor.a <= 0)
                {
                    fadeOutImage.gameObject.SetActive(false); // Disattiva l'immagine di fade-out
                }
            }
        }
        else
        {
            // Ruota l'immagine in senso orario
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // Metodo per impostare la condizione da un altro script
    public void SetDisappearCondition(bool condition)
    {
        shouldDisappear = condition;
    }
}
