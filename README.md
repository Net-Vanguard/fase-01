<details>
<summary>English version 🇺🇸</summary>


<h1 align="center">
  <br>
  <img src="https://github.com/user-attachments/assets/54f21caa-4fdb-4cb8-a282-104bda580d30" alt="ContactRegionator Logo" width="180">
  <br>
  FIAP Cloud Games (FCG) - Phase 2
  <br>
</h1>

<h4 align="center">
Service developed as part of the Phase 2 Tech Challenge — <a href="https://www.fiap.com.br/" target="_blank">FIAP</a> Postgraduate in .NET Software Architecture with Azure.
</h4>

<p align="center">
  <a href="#✨-key-features">✨ Key Features</a> •
  <a href="#🧠-technical-requirements">🧠 Technical Requirements</a> •
  <a href="#🚀-how-to-use">🚀 How to Use</a>
</p>

<hr>

[![Notion](https://img.shields.io/badge/Notion-Tech%20Challenge%20Phase%202-000000?style=for-the-badge&logo=notion)](https://taisprestes01.notion.site/Desafio-1-1e4d3ce3193b8016a48ad266e08f6ccc) <h2>✨ Key Features</h2>

<ul>
<li><strong>Scalability and Resilience</strong>: Infrastructure chosen to support a high number of users.</li>
<li><strong>Application Containerization (Docker)</strong>: Creation of a simple and small Docker image for easy deployments.</li>
<li><strong>Automated CI/CD</strong>: Pipelines for continuous integration and continuous delivery.</li>
<li><strong>Cloud Deployment</strong>: Application published and updated via pipeline in a chosen cloud provider (AWS, Azure, or others).</li>
<li><strong>Application Monitoring</strong>: Metrics collected to understand resource issues and application behavior.</li>
<li><strong>Monolithic Architecture</strong>: Continued use of a monolithic architecture for agile development and cloud implementation focus.</li>
</ul>

<h2>🧠 Technical Requirements</h2>

<ul>
<li><strong>CI/CD Configuration</strong>:
    <ul>
    <li>CI pipeline: Executed on PR opening/commit.</li>
    <li>CD pipeline: Executed when merge occurs in the main branch.</li>
    <li>(Optional) Multistage pipeline: Can combine CI/CD into one pipeline.</li>
    </ul>
</li>
<li><strong>Dockerization</strong>:
    <ul>
    <li>Dockerfile creation for FCG image publication to the cloud.</li>
    <li>Docker image pushed and stored in a repository (e.g., Dockerhub, ECR, ACR).</li>
    </ul>
</li>
<li><strong>Cloud Publication</strong>:
    <ul>
    <li>Application updated via pipeline.</li>
    <li>Choice of cloud provider: AWS (Free tier/AWS Academy), Azure Cloud (student license), or other preferred cloud.</li>
    </ul>
</li>
<li><strong>Monitoring</strong>:
    <ul>
    <li>Use of a monitoring stack (e.g., Prometheus, Zabbix, Datadog, New Relic, Grafana) to collect application metrics.</li>
    </ul>
</li>
</ul>

<h2>🚀 How to Use</h2>

<p><em>(Note: Specific instructions for running the deployed application will depend on the chosen cloud provider and monitoring tools. This section should be updated with actual steps once the infrastructure is in place.)</em></p>

<ol>
<li><strong>Clone the repository</strong>:
    <pre><code>git clone https://github.com/Net-Vanguard/fase-02.git
    cd fase-02
    </code></pre>
</li>
<li><strong>Review CI/CD Pipelines</strong>: Inspect the CI/CD pipeline files (e.g., <code>pipeline-ci.yml</code>, <code>pipeline-cd.yml</code> or a single <code>multistage-pipeline.yml</code>) to understand the automation flow.</li>
<li><strong>Docker Image</strong>: Build the Docker image using the provided <code>Dockerfile</code> and push it to your chosen Docker repository.</li>
<li><strong>Deploy to Cloud</strong>: The CD pipeline will handle the deployment to the selected cloud provider. Ensure your cloud environment is configured correctly.</li>
<li><strong>Access the Deployed Application</strong>: Once deployed, access the application via the provided URL from your cloud provider.</li>
<li><strong>Monitor the Application</strong>: Use the chosen monitoring stack to observe application metrics and infrastructure health.</li>
<li><strong>Review Documentation</strong>: Refer to the <code>README.md</code> for comprehensive usage instructions and objectives.</li>
</ol>

</details>

<h1 align="center">
  <br>
  <img src="https://github.com/user-attachments/assets/54f21caa-4fdb-4cb8-a282-104bda580d30" alt="ContactRegionator Logo" width="180">
  <br>
  FIAP Cloud Games (FCG) - Fase 2
  <br>
</h1>

<h4 align="center">
Aplicativo desenvolvido como parte do Tech Challenge da Fase 2 — Pós-graduação <a href="https://www.fiap.com.br/" target="_blank">FIAP</a> em Arquitetura de Software .NET com Azure.
</h4>

<p align="center">
  <a href="#✨-principais-características">✨ Principais Características</a> •
  <a href="#🧠-requisitos-técnicos">🧠 Requisitos Técnicos</a> •
  <a href="#🚀-como-usar">🚀 Como Usar</a>
</p>

<hr>

[![Notion](https://img.shields.io/badge/Notion-Tech%20Challenge%20Fase%202-000000?style=for-the-badge&logo=notion)](https://taisprestes01.notion.site/Desafio-1-1e4d3ce3193b8016a48ad266e08f6ccc) <h2>✨ Principais Características</h2>

<ul>
<li><strong>Escalabilidade e Resiliência</strong>: Infraestrutura escolhida para suportar um alto número de usuários.</li>
<li><strong>Conteinerização da Aplicação (Docker)</strong>: Criação de uma imagem Docker simples e pequena para facilitar novos deploys.</li>
<li><strong>CI/CD Automatizado</strong>: Pipelines para integração contínua e entrega contínua.</li>
<li><strong>Publicação na Cloud</strong>: Aplicação publicada e atualizada por meio da pipeline em uma provedora de cloud de livre escolha (AWS, Azure ou outras).</li>
<li><strong>Monitoramento da Aplicação</strong>: Coleta de métricas para entender possíveis problemas de recursos e compreender o comportamento da aplicação.</li>
<li><strong>Arquitetura Monolítica</strong>: Manutenção da arquitetura monolítica para facilitar o desenvolvimento ágil e focar na implementação na cloud.</li>
</ul>

<h2>🧠 Requisitos Técnicos</h2>

<ul>
<li><strong>Configuração de CI/CD</strong>:
    <ul>
    <li>Pipeline CI: Deverá ser executada na abertura de PR/Commit.</li>
    <li>Pipeline CD: Deverá ser executada quando o merge ocorrer na branch principal.</li>
    <li>(Opcional) Multistage: Se utilizada, será considerada a união das pipelines CI/CD; uma pipeline basta.</li>
    </ul>
</li>
<li><strong>Dockerização</strong>:
    <ul>
    <li>Criação de um Dockerfile para a elaboração de imagem do FCG relacionada à publicação na cloud.</li>
    <li>Envio e armazenamento de uma imagem Docker em algum repositório (ex.: Dockerhub, ECR, ACR).</li>
    </ul>
</li>
<li><strong>Publicação na Cloud</strong>:
    <ul>
    <li>A aplicação deve ser atualizada por meio da pipeline.</li>
    <li>Livre escolha da provedora de cloud: AWS (Free tier ou AWS Academy), Azure cloud (licença de estudante) ou qualquer outra cloud de preferência.</li>
    </ul>
</li>
<li><strong>Monitoramento</strong>:
    <ul>
    <li>Utilizar alguma Stack de monitoramento (ex.: Prometheus, Zabbix, Datadog, New Relic, Grafana) para coletar métricas da aplicação a fim de garantir que a infraestrutura não esteja sofrendo com alto tráfego.</li>
    </ul>
</li>
</ul>

<h2>🚀 Como Usar</h2>

<p><em>(Nota: As instruções específicas para rodar a aplicação após o deploy dependerão da provedora de cloud e das ferramentas de monitoramento escolhidas. Esta seção deve ser atualizada com os passos reais assim que a infraestrutura estiver configurada.)</em></p>

<ol>
<li><strong>Clone o repositório</strong>:
    <pre><code>git clone https://github.com/Net-Vanguard/fase-02.git
    cd fase-02
    </code></pre>
</li>
<li><strong>Verifique as Pipelines CI/CD</strong>: Inspecione os arquivos das pipelines CI/CD (ex: <code>pipeline-ci.yml</code>, <code>pipeline-cd.yml</code> ou um único <code>multistage-pipeline.yml</code>) para entender o fluxo de automação.</li>
<li><strong>Imagem Docker</strong>: Construa a imagem Docker usando o <code>Dockerfile</code> fornecido e envie-a para o seu repositório Docker escolhido.</li>
<li><strong>Deploy para a Cloud</strong>: A pipeline de CD será responsável pelo deploy para a provedora de cloud selecionada. Certifique-se de que seu ambiente na cloud esteja configurado corretamente.</li>
<li><strong>Acesse a Aplicação Deployed</strong>: Uma vez que a aplicação esteja deployed, acesse-a através da URL fornecida pela sua provedora de cloud.</li>
<li><strong>Monitore a Aplicação</strong>: Utilize a Stack de monitoramento escolhida para observar as métricas da aplicação e a saúde da infraestrutura.</li>
<li><strong>Consulte a Documentação</strong>: Consulte o <code>README.md</code> para instruções de uso e objetivos completos.</li>
</ol>
