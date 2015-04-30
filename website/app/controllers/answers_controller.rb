class AnswersController  < ApplicationController
  before_filter :authenticate_student!


  def create
    @answer = StudentAnswerQuestion.new answer_params
    @answer.student = current_student
    unless @answer.save
      redirect_to root, alert: 'Something went wrong'
    end


    subject = @answer.question.subject
    # if the student clicked next, then render another question
    if params[:commit] == 'Next Question'
      redirect_to action: 'new', subject_id: subject.id
    else
      redirect_to subject
    end
  end
  

  def quiz
    subject = Subject.find params[:subject_id]
    # if session[questions_subject_symbol] is not assigned yet, then assign it to the suffled ids of the subject's questions.
    session[questions_subject_symbol] ||= subject.questions.map(&:id).shuffle
    redirect_to action: 'new', subject_id: subject.id
  end


  def new
    unless more_questions_exist?
      redirect_to Subject.find(params[:subject_id]), notice: 'No questions to answer at the moment, comeback later.'
      return
    end
    @question = Question.find session[questions_subject_symbol].shift
  end


  private

  
  def answer_params
    params.permit(:question_id, :answer, :belongs_to, :belongs_to, :ans, :correct)
  end


  def questions_subject_symbol
    "subject_#{params[:subject_id]}_questions_ids".to_sym
  end

  
  def more_questions_exist?
    !session[questions_subject_symbol].empty?
  end
end
  
